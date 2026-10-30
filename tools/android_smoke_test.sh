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

adb logcat -c
adb shell monkey -p com.nexo.educational 1 2>&1 | tee diagnostics/android/launch.txt
launch_rc=${PIPESTATUS[0]}
sleep 15
adb shell dumpsys activity activities > diagnostics/android/activities.txt
adb logcat -d -v threadtime > diagnostics/android/logcat.txt
adb exec-out screencap -p > diagnostics/android/screenshot.png
adb shell run-as com.nexo.educational cat files/nexo-runtime.log > diagnostics/android/nexo-runtime.log 2>/dev/null
if [ ! -s diagnostics/android/nexo-runtime.log ]; then
  adb shell "find /sdcard/Android/data/com.nexo.educational -name nexo-runtime.log -print -exec cat {} \\;" > diagnostics/android/nexo-runtime-external.log 2>/dev/null
fi

grep -E "FATAL EXCEPTION|SIGSEGV|Unity|Nexo|AndroidRuntime" diagnostics/android/logcat.txt | tail -n 200 || true
exit "${launch_rc}"
