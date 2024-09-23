using System.Reflection;
using NetArchTest.Rules;

namespace ToAToa.Tests;

public class Architecture
{
    private readonly Assembly _dataAccessAssembly = typeof(DataAccess.DependencyInjection).Assembly;
    private readonly Assembly _domainAssembly = typeof(Domain.DependencyInjection).Assembly;
    private readonly Assembly _presentationAssembly = typeof(Presentation.DependencyInjection).Assembly;

    [Fact]
    public void CamadaDomain_NaoDeveTerDependenciaEm_CamadaDataAccess()
    {
        var resultado = Types.InAssembly(_domainAssembly)
            .ShouldNot()
            .HaveDependencyOn(_dataAccessAssembly.GetName().Name)
            .GetResult()
            .IsSuccessful;
        
        Assert.True(resultado);
    }
    
    [Fact]
    public void CamadaDomain_NaoDeveTerDependenciaEm_CamadaPresentation()
    {
        var resultado = Types.InAssembly(_domainAssembly)
            .ShouldNot()
            .HaveDependencyOn(_presentationAssembly.GetName().Name)
            .GetResult()
            .IsSuccessful;
        
        Assert.True(resultado);
    }
    
    [Fact]
    public void CamadaDataAccessDomain_NaoDeveTerDependenciaEm_CamadaPresentation()
    {
        var resultado = Types.InAssembly(_dataAccessAssembly)
            .ShouldNot()
            .HaveDependencyOn(_presentationAssembly.GetName().Name)
            .GetResult()
            .IsSuccessful;
        
        Assert.True(resultado);
    }
}
