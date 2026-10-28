# Fase 15 — Diagnóstico e correção de espaço GameCI

O run `40659354080` falhou antes de abrir o Unity. Os logs dos jobs EditMode e PlayMode mostram `docker: failed to register layer ... no space left on device` ao baixar `unityci/editor:ubuntu-6000.3.0f1-linux-il2cpp-3`.

A correção libera espaço no runner antes do checkout/GameCI usando `jlumbroso/free-disk-space@v1.3.1` nos workflows de testes e Android. Nenhuma alteração de código ou packages foi necessária.
