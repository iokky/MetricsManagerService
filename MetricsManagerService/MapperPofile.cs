using AutoMapper;
using MetricsManagerService.Models.Dto;
using MetricsManagerService.Models;
using MetricsManagerService.Models.Requests;

namespace MetricsManagerService;

public class MapperPofile: Profile
{
    public MapperPofile()
    {
        //Cpu map
        this.CreateMap<CpuMetrics, CpuMetricsDto>().
            ForMember(cpu => cpu.Time, opt =>
                opt.MapFrom(src => TimeSpan.FromSeconds(src.Time)));

/*        this.CreateMap<CpuMetricsCreateRequest, CpuMetrics>().
            ForMember(req => req.Time, opt =>
                opt.MapFrom(src => src.Time.TotalSeconds));*/



        //Hdd map
        this.CreateMap<HddMetrics, HddMetricsDto>().
            ForMember(dot => dot.Time, opt =>
                opt.MapFrom(src => TimeSpan.FromSeconds(src.Time)));

/*        this.CreateMap<HddMetricsCreateRequest, HddMetrics>().
            ForMember(req => req.Time, opt =>
                opt.MapFrom(src => src.Time.TotalSeconds));

        //Network map
        this.CreateMap<NetworkMetrics, NetworkMetricsDto>().
            ForMember(dot => dot.Time, opt =>
                opt.MapFrom(src => TimeSpan.FromSeconds(src.Time)));

        this.CreateMap<NetworkMetricsCreateRequest, NetworkMetrics>().
            ForMember(req => req.Time, opt =>
                opt.MapFrom(src => src.Time.TotalSeconds));

        //Ram map
        this.CreateMap<RamMetrics, RamMetricsDto>().
            ForMember(dot => dot.Time, opt =>
                opt.MapFrom(src => TimeSpan.FromSeconds(src.Time)));

        this.CreateMap<RamMetricsCreateReqest, RamMetrics>().
            ForMember(req => req.Time, opt =>
                opt.MapFrom(src => src.Time.TotalSeconds));*/

    }
}
