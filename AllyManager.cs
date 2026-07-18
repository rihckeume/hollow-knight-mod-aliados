using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InimigosViramAliados
{
    /// <summary>
    /// Gerenciador central de aliados.
    /// Mantém registro de inimigos convertidos e gerencia limites de performance.
    /// </summary>
    public static class AllyManager
    {
        /// <summary>
        /// Dicionário de aliados ativos (ID -> GameObject)
        /// </summary>
        private static Dictionary<string, GameObject> _allies = new Dictionary<string, GameObject>();

        /// <summary>
        /// Limite máximo de aliados simultâneos para performance
        /// </summary>
        public const int MAX_ALLIES = 15;

        /// <summary>
        /// Raio em unidades do jogo para buscar inimigos vizinhos
        /// </summary>
        public const float RECRUITMENT_RADIUS = 3.5f;

        /// <summary>
        /// Registra um novo aliado
        /// </summary>
        public static void RegisterAlly(GameObject enemy, string allyId)
        {
            if (!_allies.ContainsKey(allyId))
            {
                _allies[allyId] = enemy;
            }
        }

        /// <summary>
        /// Remove um aliado do registro (quando destruído ou cena muda)
        /// </summary>
        public static void UnregisterAlly(string allyId)
        {
            if (_allies.ContainsKey(allyId))
            {
                _allies.Remove(allyId);
            }
        }

        /// <summary>
        /// Retorna número atual de aliados ativos
        /// </summary>
        public static int GetAllyCount()
        {
            // Limpar referências nulas
            var deadAllies = _allies.Where(kvp => kvp.Value == null).Select(kvp => kvp.Key).ToList();
            foreach (var id in deadAllies)
            {
                _allies.Remove(id);
            }
            return _allies.Count;
        }

        /// <summary>
        /// Verifica se atingiu limite de aliados
        /// </summary>
        public static bool IsAllyLimitReached()
        {
            return GetAllyCount() >= MAX_ALLIES;
        }

        /// <summary>
        /// Retorna lista de aliados vizinhos a uma posição
        /// </summary>
        public static List<GameObject> GetNearbyAllies(Vector3 position, float radius)
        {
            return _allies.Values
                .Where(ally => ally != null && Vector3.Distance(ally.transform.position, position) < radius)
                .ToList();
        }

        /// <summary>
        /// Limpa todos os aliados (ex: reset de cena)
        /// </summary>
        public static void ClearAllAllies()
        {
            _allies.Clear();
        }

        /// <summary>
        /// Log de debug: imprime status de aliados
        /// </summary>
        public static void LogAllyStatus()
        {
            Modding.Logger.Log($"[Aliados] Count: {GetAllyCount()}/{MAX_ALLIES}");
        }
    }
}