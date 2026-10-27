"""Gera a impressão digital determinística dos arquivos-fonte da release."""
import hashlib
import subprocess
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
EXCLUDED = {"docs/RELEASE_MANIFEST.md"}


def main():
    output = hashlib.sha256()
    files = []
    for path in sorted(ROOT.rglob("*")):
        if not path.is_file() or ".git" in path.parts or path.name.endswith(".meta"):
            continue
        relative = path.relative_to(ROOT).as_posix()
        if relative in EXCLUDED or relative.startswith("build/") or relative.startswith("Library/"):
            continue
        files.append(relative)
        data = path.read_bytes()
        output.update(relative.encode("utf-8"))
        output.update(b"\0")
        output.update(data)
        output.update(b"\0")
    commit = subprocess.check_output(["git", "rev-parse", "HEAD"], cwd=ROOT, text=True).strip()
    print("release=0.1.0-rc1")
    print("unity=6000.3.0f1")
    print("source_fingerprint_sha256=" + output.hexdigest())
    print("git_commit=" + commit)
    print("source_file_count=" + str(len(files)))
    print("artifacts=not-generated")


if __name__ == "__main__":
    main()
