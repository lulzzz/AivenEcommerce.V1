using AivenEcommerce.V1.Application.Mappers.Invoices;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Invoices;

using System;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Invoices
{
    public class InvoiceDtoMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_InvoiceNotNull_ReturnSameId()
        {
            Invoice invoice = MockInvoice();

            InvoiceDto invoiceDto = invoice.ConvertToDto();

            Assert.Equal(invoice.Id, invoiceDto.Id);
        }

        [Fact]
        public void ConvertToDto_InvoiceNotNull_ReturnSameLink()
        {
            Invoice invoice = MockInvoice();

            InvoiceDto invoiceDto = invoice.ConvertToDto();

            Assert.Equal(invoice.Link, invoiceDto.Link);
        }

        [Fact]
        public void ConvertToDto_InvoiceNotNull_ReturnSameTransaction()
        {
            Invoice invoice = MockInvoice();

            InvoiceDto invoiceDto = invoice.ConvertToDto();

            Assert.Equal(invoice.Transaction, invoiceDto.Transaction);
        }

        [Fact]
        public void ConvertToDto_InvoiceNotNull_ReturnSameOrderId()
        {
            Invoice invoice = MockInvoice();

            InvoiceDto invoiceDto = invoice.ConvertToDto();

            Assert.Equal(invoice.OrderId, invoiceDto.OrderId);
        }

        [Fact]
        public void ConvertToDto_InvoiceNotNull_ReturnSamePaymentProviderWebhookReceived()
        {
            Invoice invoice = MockInvoice();

            InvoiceDto invoiceDto = invoice.ConvertToDto();

            Assert.Equal(invoice.PaymentProviderWebhookReceived, invoiceDto.PaymentProviderWebhookReceived);
        }

        [Fact]
        public void ConvertToDto_InvoiceNotNull_ReturnSamePaymentProvider()
        {
            Invoice invoice = MockInvoice();

            InvoiceDto invoiceDto = invoice.ConvertToDto();

            Assert.Equal(invoice.PaymentProvider, invoiceDto.PaymentProvider);
        }

        private Invoice MockInvoice() =>
        new()
        {
            Id = Guid.NewGuid(),
            Transaction = nameof(Invoice.Transaction),
            OrderId = nameof(Invoice.OrderId),
            PaymentProviderWebhookReceived = true,
            PaymentProvider = Domain.Shared.Enums.PaymentProvider.MercadoPago,
            Link = new("http://contoso.com")
        };
    }
}
