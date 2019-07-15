using Carpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Interfaces
{
    public interface ICarSharingRepository
    {
        //IEnumerable<CarSharing> CarSharings { get; set; }
        List<CarSharing> CarSharings { get; set; }
        void SaveCarSharing(CarSharing carSharing);
    }
}
