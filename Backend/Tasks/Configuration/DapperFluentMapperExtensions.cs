using Dapper.FluentMap;
using Tasks.DapperMappers;

namespace Tasks.Configuration
{
    public static class DapperFluentMapperExtensions
    {
        public static void AddDapperFluentMappers()
        {
            //Dapper Fluent Mappers
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new TaskGridDomainModelMap());
                config.AddMap(new TaskDetailsDomainModelMap());
            });
        }
    }
}
