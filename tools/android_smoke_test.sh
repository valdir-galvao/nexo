#!/usr/bin/env bash
set -u

apk_path="$(find . -type f -name 'Nexo.apk' -print -quit)"
if [ -z "${apk_path}" ]; then
  echo "Nexo.apk not found" >&2
  exit 20
fi

mkdir -p diagnostics/android
echo "apk=${apk_path}" | tee diagnostics/android/run.txt
adb wait-for-device
adb shell getprop ro.product.cpu.abi | tee -a diagnostics/android/run.txt
adb shell getprop ro.build.version.sdk | tee -a diagnostics/android/run.txt

set +e
adb install -r "${apk_path}" 2>&1 | tee diagnostics/android/install.txt
install_rc=${PIPESTATUS[0]}
if [ "${install_rc}" -ne 0 ]; then
  adb logcat -d -v threadtime > diagnostics/android/logcat-install-failure.txt
  exit "${install_rc}"
fi

# The first fullscreen launch on an AVD can display Android's one-time
# immersive-mode education overlay over the application. Confirm that system
# prompt before launching so screenshots and interaction evidence represent
# the app rather than the emulator's instructional UI. This is emulator-only
# state and does not change the application or device production settings.
adb shell settings put secure immersive_mode_confirmations confirmed 2>/dev/null || true
adb logcat -c
adb shell monkey -p com.nexo.educational 1 2>&1 | tee diagnostics/android/launch.txt
launch_rc=${PIPESTATUS[0]}
sleep 20
# Pixel Launcher can show its own ANR dialog on a cold AVD and obscure a
# correctly running Unity activity. Dismiss only that known system dialog,
# then relaunch and verify that Unity is the foreground activity before the
# screenshot is captured.
if adb shell dumpsys window windows 2>/dev/null | grep -q "Application Not Responding: com.google.android.apps.nexuslauncher"; then
  adb shell input keyevent 4 || true
  adb shell monkey -p com.nexo.educational 1 >/dev/null 2>&1 || true
  sleep 5
fi
adb shell dumpsys activity activities > diagnostics/android/activities.txt
adb logcat -d -v threadtime > diagnostics/android/logcat.txt
adb exec-out screencap -p > diagnostics/android/screenshot.png
adb shell run-as com.nexo.educational cat /storage/emulated/0/Android/data/com.nexo.educational/files/nexo-runtime.log > diagnostics/android/nexo-runtime.log 2>/dev/null
if [ ! -s diagnostics/android/nexo-runtime.log ]; then
  adb shell "find /sdcard/Android/data/com.nexo.educational -name nexo-runtime.log -print -exec cat {} \\;" > diagnostics/android/nexo-runtime-external.log 2>/dev/null
fi

foreground="$(adb shell dumpsys activity activities 2>/dev/null | grep -m 1 -E 'ResumedActivity:.*com.nexo.educational|topResumedActivity=.*com.nexo.educational' || true)"
printf '%s\n' "foreground=${foreground}" | tee -a diagnostics/android/run.txt
test -n "${foreground}"

# The generated intro button is centered in the landscape safe area. Tap it,
# wait for the fade/BuildWorld transition, and keep a second screenshot as
# evidence that the first interaction is visible on the emulator.
adb shell input tap 1080 700
sleep 3
adb exec-out screencap -p > diagnostics/android/screenshot-after-start.png

grep -E "FATAL EXCEPTION|SIGSEGV|Unity|Nexo|AndroidRuntime" diagnostics/android/logcat.txt | tail -n 200 || true
exit "${launch_rc}"
