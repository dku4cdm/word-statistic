using System.Collections.Generic;

namespace WordStatistic.Domain.Entities
{
    /// <summary>
    /// Use only for demonstration a storage
    /// </summary>
    public static class Memory
    {
        public static Dictionary<string, int> Data { get; set; } = new Dictionary<string, int>();
    }
}
