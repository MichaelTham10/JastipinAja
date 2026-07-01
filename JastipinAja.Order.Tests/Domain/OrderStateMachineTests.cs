using System;
using System.Collections.Generic;
using System.Text;
using global::JastipinAja.BuildingBlocks.Exceptions;
using global::JastipinAja.Order.Domain;
using JastipinAja.BuildingBlocks.Exceptions;
using JastipinAja.Order.Domain;
using Xunit;

namespace JastipinAja.Order.Tests.Domain
{
    public class OrderStateMachineTests
    {
        // helper: bikin order valid untuk dipakai berulang di banyak test
        private static Order.Domain.Order CreateValidOrder() =>
            new(Guid.NewGuid(), "ORD-2026-00001", "Sepatu", 500_000m);

        [Fact]
        public void OrderBaru_HarusBerstatusRequested()
        {
            var order = CreateValidOrder();

            Assert.Equal(OrderStatus.Requested, order.Status);
        }

        [Fact]
        public void Accept_DariRequested_Berhasil()
        {
            var order = CreateValidOrder();

            order.Accept();

            Assert.Equal(OrderStatus.Accepted, order.Status);
        }

        [Fact]
        public void Complete_TanpaMelewatiTahapan_HarusDitolak()
        {
            var order = CreateValidOrder();  // status masih Requested

            // memanggil Complete dari Requested itu ilegal → harus lempar DomainException
            var ex = Assert.Throws<DomainException>(() => order.Complete());
            Assert.Contains("Transisi tidak valid", ex.Message);
        }

        [Fact]
        public void AlurLengkap_HinggaCompleted_Berhasil()
        {
            var order = CreateValidOrder();

            order.Accept();
            order.MarkAsPaid();
            order.MarkAsPurchased();
            order.Ship();
            order.MarkReadyForHandover();
            order.Complete();

            Assert.Equal(OrderStatus.Completed, order.Status);
        }
    }
}
