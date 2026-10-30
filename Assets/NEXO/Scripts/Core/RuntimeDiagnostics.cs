using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nexo.Core
{
    /// <summary>Registro local de falhas para diagnosticar crashes e erros de inicializa??o sem rede.</summary>
    public sealed class RuntimeDiagnostics : MonoBehaviour
    {
        private const long MaxLogBytes = 2 * 1024 * 1024;
        private static RuntimeDiagnostics instance;
        private readonly object sync = new object();
        private readonly Queue<string> pending = new Queue<string>();
        private string logPath;
        private string lastError;
        private string currentSceneName = "<boot>";
        private bool shuttingDown;

        public static string LogPath { get { return instance == null ? string.Empty : instance.logPath; } }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Install()
        {
            if (instance != null) return;
            var host = new GameObject("Nexo Runtime Diagnostics");
            instance = host.AddComponent<RuntimeDiagnostics>();
            DontDestroyOnLoad(host);
        }

        private void Awake()
        {
            try
            {
                logPath = Path.Combine(Application.persistentDataPath, "nexo-runtime.log");
                PrepareLog();
                Application.logMessageReceivedThreaded += CaptureLog;
                SceneManager.sceneLoaded += OnSceneLoaded;
                WriteImmediate("SESSION_START", BuildEnvironmentSummary());
            }
            catch (Exception exception)
            {
                // N?o relogar no Unity para evitar recurs?o durante uma falha de diagn?stico.
                lastError = "Falha ao iniciar diagn?stico: " + exception.GetType().Name;
            }
        }

        private void Update()
        {
            FlushPending(20);
        }

        private void OnApplicationQuit()
        {
            shuttingDown = true;
            FlushPending(int.MaxValue);
            WriteImmediate("SESSION_END", "scene=" + currentSceneName);
            Application.logMessageReceivedThreaded -= CaptureLog;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnDestroy()
        {
            if (!shuttingDown) Application.logMessageReceivedThreaded -= CaptureLog;
        }

        private void CaptureLog(string condition, string stackTrace, LogType type)
        {
            if (type != LogType.Error && type != LogType.Exception && type != LogType.Assert && type != LogType.Warning) return;
            var line = string.Format("[{0:O}] [{1}] scene={2} message={3}\n{4}", DateTime.UtcNow, type, currentSceneName, condition, stackTrace);
            lock (sync)
            {
                if (pending.Count < 200) pending.Enqueue(line);
            }
            if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert) lastError = condition;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            currentSceneName = scene.name;
            Enqueue("SCENE_LOADED mode=" + mode + " scene=" + scene.name);
        }

        private void Enqueue(string line)
        {
            lock (sync)
            {
                if (pending.Count < 200) pending.Enqueue("[" + DateTime.UtcNow.ToString("O") + "] " + line + "\n");
            }
        }

        private void FlushPending(int maxLines)
        {
            for (var i = 0; i < maxLines; i++)
            {
                string line = null;
                lock (sync)
                {
                    if (pending.Count > 0) line = pending.Dequeue();
                }
                if (line == null) break;
                AppendSafe(line);
            }
        }

        private void PrepareLog()
        {
            var directory = Path.GetDirectoryName(logPath);
            if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
            if (File.Exists(logPath) && new FileInfo(logPath).Length > MaxLogBytes)
                File.Delete(logPath);
        }

        private void WriteImmediate(string category, string message)
        {
            AppendSafe("[" + DateTime.UtcNow.ToString("O") + "] [" + category + "] " + message + "\n");
        }

        private void AppendSafe(string line)
        {
            try
            {
                if (string.IsNullOrEmpty(logPath)) return;
                if (File.Exists(logPath) && new FileInfo(logPath).Length > MaxLogBytes) return;
                File.AppendAllText(logPath, line, Encoding.UTF8);
            }
            catch (Exception exception)
            {
                lastError = "Falha ao gravar log: " + exception.GetType().Name;
            }
        }

        private static string BuildEnvironmentSummary()
        {
            return string.Format("unity={0} platform={1} device={2} os={3} graphics={4} memoryMB={5} dataPath={6}",
                Application.unityVersion, Application.platform, SystemInfo.deviceModel, SystemInfo.operatingSystem,
                SystemInfo.graphicsDeviceName, SystemInfo.systemMemorySize, Application.persistentDataPath);
        }

        private void OnGUI()
        {
            if (string.IsNullOrEmpty(lastError) || Application.isBatchMode) return;
            GUI.color = new Color(1f, 0.8f, 0.35f, 1f);
            GUI.Label(new Rect(16f, 16f, Screen.width - 32f, 48f), "Erro registrado. Diagn?stico salvo em: " + logPath);
            GUI.color = Color.white;
        }
    }
}
