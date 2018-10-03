using Api.Logic;
using Api.Models.Network;
using Api.Repository;
using Autofac;

namespace Api
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CashFlowRepository>().As<ICashFlowRepository>();
            builder.RegisterType<NodeRepository>().As<INodeRepository>();
            builder.RegisterType<UniformRandomRepository>().As<IUniformRandomRepository>();

            builder.RegisterType<NodeSimulator>().As<INodeSimulator>();
            builder.RegisterType<SimulationEvaluator>().As<ISimulationEvaluator>();
            builder.RegisterType<Network>().As<INetwork>();
            base.Load(builder);
        }
    }
}
