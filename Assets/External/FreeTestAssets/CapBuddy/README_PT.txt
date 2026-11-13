CAPBUDDY — PROTOTIPO 3D v0.1
Personagem inspirado no bichinho de bone escolhido. Modelo procedural estilizado, sem fidelidade fotografica; nao e uma conversao direta da imagem.

ARQUIVOS
CapBuddy.blend: fonte editavel com cenario de apresentacao.
CapBuddy.fbx: personagem com esqueleto e animacoes para Unity.
CapBuddy.glb: alternativa com materiais e textura incorporados.
Textures/Fur_BaseColor.png: textura simples de cor do pelo; demais superficies usam materiais PBR de cores solidas.
Preview.png e animacoes GIF: apresentacao do modelo real renderizado em Blender.
build.py: gerador reproduzivel, Blender 4.3.2.
Unity/Editor/CapBuddySetup.cs: gerador de prefab e AnimatorController dentro da Unity.

ANIMACOES (24 fps)
Idle: 48 quadros; Walk: 24; Investigate: 72; Success: 60.
Walk e no lugar; deslocamento deve ser feito pelo controlador do jogo.
Rig customizado GENERIC. Nao e um Avatar Humanoid validado. Pecas tem pesos rigidos por osso, sem deformacao organica de alta qualidade.

UNITY
1. Copie CapBuddy.fbx, Textures e Unity/Editor para Assets/CapBuddy.
2. No importador FBX, use Rig > Generic.
3. Em Animation, confira os quatro clips e habilite Loop Time somente para Idle e Walk. Apply.
4. Execute Tools > CapBuddy > Create Prefab and Animator.
5. Arraste o prefab criado para sua cena. Parametro Speed > 0.1 ativa Walk; triggers Investigate e Success ativam acoes.
6. Confira materiais; para URP, pode ser necessario converter/reatribuir materiais no projeto.
Nao inclui controlador de jogador, colisores, audio ou integracao com o seu repositorio. O script Unity foi fornecido sem execucao em Unity neste ambiente.

LIMITACOES
Prototipo com formas geometricas sobrepostas; rosto estatico, sem lip sync, sem cabelo por fios, sem LODs e sem otimizacao final mobile. Revisar proporcoes e animacoes antes de producao.
Modelo e textura criados proceduralmente para esta entrega, sem assets de terceiros incorporados. Nao inclui certificacao juridica de exclusividade da referencia visual.
