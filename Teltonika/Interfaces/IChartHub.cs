using System.Threading.Tasks;

namespace Teltonika.Interfaces
{
    public interface IChartHub
    {
        Task ChartStatisticsChange(int num, string m);
    }
}
