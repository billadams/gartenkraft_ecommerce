using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Gartenkraft.Models
{
    public class CheckoutDB
    {
        public int SaveShippingOrder(Shipping oShippingData)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            int newid = 0;
            string insertString =
                "INSERT into tblShipping (customer_id, shipping_address1, shipping_address2, shipping_city, shipping_state, shipping_zip, shipping_zip4, shipping_country)" +
                " values (@customer_id, @shipping_address1, @shipping_address2, @shipping_city, @shipping_state, @shipping_zip, @shipping_zip4, @shipping_country)SELECT SCOPE_IDENTITY()";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);
            insertCommand.Parameters.AddWithValue("@customer_id", oShippingData.CustomerID);
            insertCommand.Parameters.AddWithValue("@shipping_address1", oShippingData.ShippingAddress);
            insertCommand.Parameters.AddWithValue("@shipping_address2", oShippingData.ShippingAddress2);
            insertCommand.Parameters.AddWithValue("@shipping_city", oShippingData.ShippingCity);
            insertCommand.Parameters.AddWithValue("@shipping_state", oShippingData.ShippingState);
            insertCommand.Parameters.AddWithValue("@shipping_zip", oShippingData.ShippingZip);
            insertCommand.Parameters.AddWithValue("@shipping_zip4", oShippingData.ShippingZip4);
            insertCommand.Parameters.AddWithValue("@shipping_country", oShippingData.ShippingCountry);
            try
            {
                oConnection.Open();
                newid = Convert.ToInt32(insertCommand.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }

            finally
            {
                oConnection.Close();
            }
            return newid;
        }

        public int SaveBillingOrder(BillingInfo oBillingInfo)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            int newid = 0;
            string insertString =
                "INSERT into tblBilling_Information (customer_id, billing_address1, billing_address2, billing_city, billing_state, billing_zip, billing_zip4, billing_country)" +
                " values (@customer_id, @billing_address1, @billing_address2, @billing_city, @billing_state, @billing_zip, @billing_zip4, @billing_country)SELECT SCOPE_IDENTITY()";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);
            insertCommand.Parameters.AddWithValue("@customer_id", oBillingInfo.CustomerID);
            insertCommand.Parameters.AddWithValue("@billing_address1", oBillingInfo.BillingAddress);
            insertCommand.Parameters.AddWithValue("@billing_address2", oBillingInfo.BillingAddress2);
            insertCommand.Parameters.AddWithValue("@billing_city", oBillingInfo.BillingCity);
            insertCommand.Parameters.AddWithValue("@billing_state", oBillingInfo.BillingState);
            insertCommand.Parameters.AddWithValue("@billing_zip", oBillingInfo.BillingZip);
            insertCommand.Parameters.AddWithValue("@billing_zip4", oBillingInfo.BillingZip4);
            insertCommand.Parameters.AddWithValue("@billing_country", oBillingInfo.BillingCountry);
            try
            {
                oConnection.Open();
                newid = Convert.ToInt32(insertCommand.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }

            finally
            {
                oConnection.Close();
            }
            return newid;
        }

        public int SaveInvoice(SalesInvoice oInvoiceInfo)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            int newid = 0;
            string insertString =
                "INSERT into tblSales_Invoice (customer_id, invoice_date, billing_id, shipping_id)" +
                " values (@customer_id, @invoice_date, @billing_id, @shipping_id)SELECT SCOPE_IDENTITY()";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);
            insertCommand.Parameters.AddWithValue("@customer_id", oInvoiceInfo.CustomerID);
            insertCommand.Parameters.AddWithValue("@invoice_date", oInvoiceInfo.InvoiceDate);
            insertCommand.Parameters.AddWithValue("@billing_id", oInvoiceInfo.BillingID);
            insertCommand.Parameters.AddWithValue("@shipping_id", oInvoiceInfo.ShippingID);
            try
            {
                oConnection.Open();
                newid = Convert.ToInt32(insertCommand.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }

            finally
            {
                oConnection.Close();
            }
            return newid;
        }
    }
}