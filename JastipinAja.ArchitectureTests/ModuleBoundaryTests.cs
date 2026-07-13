using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JastipinAja.ArchitectureTests
{
    public class ModuleBoundaryTests
    {
        private static readonly Assembly OrderAssembly = Assembly.Load("JastipinAja.Order");
        private static readonly Assembly OrderContractsAssembly = Assembly.Load("JastipinAja.Order.Contracts");
        private static readonly Assembly PaymentAssembly = Assembly.Load("JastipinAja.Payment");
        private static readonly Assembly PaymentContractsAssembly = Assembly.Load("JastipinAja.Payment.Contracts");
        private static readonly Assembly SharedKernelAssembly = Assembly.Load("JastipinAja.SharedKernel");

        [Fact]
        public void Payment_TidakBoleh_ReferensiOrderInternal()
        {
            var referencedAssemblies = PaymentAssembly.GetReferencedAssemblies()
                .Select(a => a.Name)
                .ToList();

            Assert.DoesNotContain("JastipinAja.Order", referencedAssemblies);
        }

        [Fact]
        public void Order_TidakBoleh_ReferensiPayment()
        {
            var referencedAssemblies = OrderAssembly.GetReferencedAssemblies()
                .Select(a => a.Name)
                .ToList();

            Assert.DoesNotContain("JastipinAja.Payment", referencedAssemblies);
            Assert.DoesNotContain("JastipinAja.Payment.Contracts", referencedAssemblies);
        }

        [Fact]
        public void OrderContracts_HarusNetral_TidakBolehReferensiModulApapun()
        {
            var referencedAssemblies = OrderContractsAssembly.GetReferencedAssemblies()
                .Select(a => a.Name)
                .ToList();

            Assert.DoesNotContain("JastipinAja.Order", referencedAssemblies);
            Assert.DoesNotContain("JastipinAja.Payment", referencedAssemblies);
        }

        [Fact]
        public void PaymentContracts_HarusNetral_TidakBolehReferensiModulApapun()
        {
            var result = Types.InAssembly(PaymentContractsAssembly)
                .Should()
                .NotHaveDependencyOnAny("JastipinAja.Order", "JastipinAja.Payment")
                .GetResult();

            Assert.True(result.IsSuccessful, Pelanggar(result));
        }

        [Fact]
        public void SharedKernel_TidakBoleh_ReferensiModulManapun()
        {
            var result = Types.InAssembly(SharedKernelAssembly)
                .Should()
                .NotHaveDependencyOnAny("JastipinAja.Order", "JastipinAja.Payment")
                .GetResult();

            Assert.True(result.IsSuccessful, Pelanggar(result));
        }

        [Fact]
        public void TipeDiModulOrder_HarusInternal_KecualiRegistrarDanContracts()
        {
            var result = Types.InAssembly(OrderAssembly)
                .That().DoNotHaveNameEndingWith("ModuleServiceRegistrar")
                .And().DoNotResideInNamespace("JastipinAja.Order.Migrations")  // <- tambahan ini
                .Should().NotBePublic()
                .GetResult();

            Assert.True(result.IsSuccessful, Pelanggar(result));
        }

        private static string Pelanggar(TestResult r) =>
            r.IsSuccessful ? "" : "Pelanggar boundary: " + string.Join(", ", r.FailingTypeNames ?? []);
    }
}
