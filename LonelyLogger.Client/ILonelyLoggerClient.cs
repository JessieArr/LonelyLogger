using System.Collections.Generic;
using System.Threading.Tasks;

namespace LonelyLogger.Client
{
    public interface ILonelyLoggerClient
    {
        Task<LogResult> PostLogAsync(object objectToLog);
        Task<LogResult> PostLogsAsync(IEnumerable<object> objectsToLog);
    }
}