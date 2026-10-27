"""Valida o contrato mínimo do conteúdo pedagógico P5 sem chamar rede."""
import json
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]
CONTENT = ROOT / "Assets" / "NEXO" / "Data" / "mission-silent-reservoir-learning.json"


def validate(data):
    required = {"contentVersion", "missionId", "objectives", "diagnostic", "postTest", "transfer", "reviews"}
    missing = required.difference(data)
    if missing:
        raise ValueError(f"missing top-level keys: {sorted(missing)}")
    if data["missionId"] != "mission-silent-reservoir":
        raise ValueError("unexpected mission id")
    if not data["objectives"]:
        raise ValueError("at least one objective is required")
    ids = set()
    for objective in data["objectives"]:
        fields = {"id", "competencyId", "description", "difficulty", "prerequisites", "evidenceExpected", "criteria", "explanation", "validAlternatives", "transferMethod"}
        missing = fields.difference(objective)
        if missing:
            raise ValueError(f"objective {objective.get('id')}: missing {sorted(missing)}")
        if objective["id"] in ids:
            raise ValueError(f"duplicate objective id: {objective['id']}")
        ids.add(objective["id"])
        if not 1 <= objective["difficulty"] <= 3:
            raise ValueError(f"invalid difficulty: {objective['id']}")
    if data["reviews"] != [7, 30]:
        raise ValueError("review intervals must default to 7 and 30 days")


if __name__ == "__main__":
    validate(json.loads(CONTENT.read_text(encoding="utf-8")))
    print(f"learning_content_ok: {CONTENT}")
