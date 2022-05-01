using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teltonika.Interfaces
{
    public interface IChartHub
    {
        Task ChartStatisticsChange(int num, string m);
    }
}
