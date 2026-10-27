"""Checks de integração que não exigem Unity, rede ou dados externos."""
import json
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]


def main():
    json_files = [ROOT / "Packages" / "manifest.json", ROOT / "Packages" / "packages-lock.json"]
    json_files += list((ROOT / "Assets" / "NEXO").rglob("*.asmdef"))
    for path in json_files:
        json.loads(path.read_text(encoding="utf-8"))

    cs_files = list((ROOT / "Assets" / "NEXO").rglob("*.cs"))
    for path in cs_files:
        text = path.read_text(encoding="utf-8")
        if text.count("{") != text.count("}"):
            raise ValueError(f"brace mismatch: {path}")

    forbidden = ("UnityWebRequest", "UnityPurchasing", "Advertisement", "LocationService", "Microphone.Start")
    matches = []
    for path in cs_files:
        text = path.read_text(encoding="utf-8")
        for token in forbidden:
            if token in text:
                matches.append((str(path), token))
    # SafetyPolicy contains words describing disabled features, not integrations.
    active = [item for item in matches if item[0].replace("\\", "/").endswith("SafetyPolicy.cs") is False]
    if active:
        raise ValueError(f"forbidden active dependency markers: {active}")
    print(f"static_qa_ok: json={len(json_files)} cs={len(cs_files)}")


if __name__ == "__main__":
    main()
