using Structurizr;

namespace TeeLab.C4Model.Components;

public class PaymentGateway
{
    private C4 Project { get; set; }
    private ContextDiagram Context { get; set; }
    private ContainerDiagram Container { get; set; }
    
    private Component Infrastructure { get; set; }
    private Component Application { get; set; }
    private Component Domain { get; set; }
    private Component Interfaces { get; set; }

    public PaymentGateway(ContextDiagram context, ContainerDiagram container, C4 project)
    {
        Project = project ?? throw new ArgumentNullException(nameof(project));
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Container = container ?? throw new ArgumentNullException(nameof(container));
        
        Infrastructure = Container.PaymentGateway.AddComponent("Infrastructure", "Handles infrastructure and management.");
        Application = Container.PaymentGateway.AddComponent("Application", "Handles application and management.");
        Domain = Container.PaymentGateway.AddComponent("Domain", "Handles domain and management.");
        Interfaces = Container.PaymentGateway.AddComponent("Interfaces", "Handles interfaces and management.");
    }

    public void Generate()
    {
        Interfaces.Uses(Application, "Use");
        Application.Uses(Domain, "Use");
        Application.Uses(Infrastructure, "Use");
        
        Infrastructure.Uses(Context.Stripe,"Use");
        Infrastructure.Uses(Context.Supabase, "Use");
        
        ApplyStyles();
        Publish();
    }

    public void ApplyStyles()
    {
    }
    
    public void Publish()
    {
        ComponentView view = Project.ViewSet.CreateComponentView(Container.PaymentGateway, "Payment Gateway Component", "");
        view.Title = "Payment Gateway Component View";
        view.Add(Infrastructure);
        view.Add(Application);
        view.Add(Domain);
        view.Add(Interfaces);
        view.Add(Context.Stripe);
        view.Add(Context.Supabase);
    }
}