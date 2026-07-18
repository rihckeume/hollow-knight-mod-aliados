using UnityEngine;

namespace InimigosViramAliados
{
    /// <summary>
    /// Componente de marcação para inimigos que se tornaram aliados.
    /// Anexado a todo inimigo convertido para sinalizar seu novo status.
    /// </summary>
    public class EnemyAllyTag : MonoBehaviour
    {
        /// <summary>
        /// Identificador único do aliado (para rastreamento)
        /// </summary>
        public string AllyId { get; set; }

        /// <summary>
        /// Tempo em que foi convertido em aliado
        /// </summary>
        public float ConversionTime { get; set; }

        /// <summary>
        /// Número de inimigos recrutados por este aliado (cadeia)
        /// </summary>
        public int RecruitsCount { get; set; }

        public EnemyAllyTag()
        {
            AllyId = System.Guid.NewGuid().ToString();
            ConversionTime = Time.time;
            RecruitsCount = 0;
        }
    }
}