using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gartenkraft.Models;

namespace Gartenkraft.Helpers
{
    public class CheckoutDB
    {
        public int SaveGuestShippingOrder(Shipping oShippingData)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            int newid = 0;
            string insertString =
                "INSERT into tblShipping (shipping_address1, shipping_address2, shipping_city, shipping_state, shipping_zip, shipping_zip4, shipping_country,shipping_first_name,shipping_last_name)" +
                " values (@shipping_address1, @shipping_address2, @shipping_city, @shipping_state, @shipping_zip, @shipping_zip4, @shipping_country, @shipping_first_name, @shipping_last_name)SELECT SCOPE_IDENTITY()";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);
            //insertCommand.Parameters.AddWithValue("@customer_id", oShippingData.CustomerID);
            insertCommand.Parameters.AddWithValue("@shipping_address1", oShippingData.ShippingAddress);
            insertCommand.Parameters.AddWithValue("@shipping_address2", oShippingData.ShippingAddress2);
            insertCommand.Parameters.AddWithValue("@shipping_city", oShippingData.ShippingCity);
            insertCommand.Parameters.AddWithValue("@shipping_state", oShippingData.ShippingState);
            insertCommand.Parameters.AddWithValue("@shipping_zip", oShippingData.ShippingZip);
            insertCommand.Parameters.AddWithValue("@shipping_zip4", oShippingData.ShippingZip4);
            insertCommand.Parameters.AddWithValue("@shipping_country", oShippingData.ShippingCountry);
            insertCommand.Parameters.AddWithValue("@shipping_first_name", oShippingData.ShippingFirstName);
            insertCommand.Parameters.AddWithValue("@shipping_last_name", oShippingData.ShippingLastName);
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
        public int SaveUserShippingOrder(Shipping oShippingData)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            int newid = 0;
            string insertString =
                "INSERT into tblShipping (customer_id, shipping_address1, shipping_address2, shipping_city, shipping_state, shipping_zip, shipping_zip4, shipping_country,shipping_first_name,shipping_last_name)" +
                " values (@customer_id, @shipping_address1, @shipping_address2, @shipping_city, @shipping_state, @shipping_zip, @shipping_zip4, @shipping_country, @shipping_first_name, @shipping_last_name)SELECT SCOPE_IDENTITY()";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);
            insertCommand.Parameters.AddWithValue("@customer_id", oShippingData.CustomerID);
            insertCommand.Parameters.AddWithValue("@shipping_address1", oShippingData.ShippingAddress);
            insertCommand.Parameters.AddWithValue("@shipping_address2", oShippingData.ShippingAddress2);
            insertCommand.Parameters.AddWithValue("@shipping_city", oShippingData.ShippingCity);
            insertCommand.Parameters.AddWithValue("@shipping_state", oShippingData.ShippingState);
            insertCommand.Parameters.AddWithValue("@shipping_zip", oShippingData.ShippingZip);
            insertCommand.Parameters.AddWithValue("@shipping_zip4", oShippingData.ShippingZip4);
            insertCommand.Parameters.AddWithValue("@shipping_country", oShippingData.ShippingCountry);
            insertCommand.Parameters.AddWithValue("@shipping_first_name", oShippingData.ShippingFirstName);
            insertCommand.Parameters.AddWithValue("@shipping_last_name", oShippingData.ShippingLastName);
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

        public int SaveGuestBillingOrder(BillingInfo oBillingInfo)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            int newid = 0;
            string insertString =
                "INSERT into tblBilling_Information (billing_address1, billing_address2, billing_city, billing_state, billing_zip, billing_zip4, billing_country, billing_first_name, billing_last_name)" +
                " values (@billing_address1, @billing_address2, @billing_city, @billing_state, @billing_zip, @billing_zip4, @billing_country, @billing_first_name, @billing_last_name)SELECT SCOPE_IDENTITY()";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);
            //insertCommand.Parameters.AddWithValue("@customer_id", oBillingInfo.CustomerID);
            insertCommand.Parameters.AddWithValue("@billing_address1", oBillingInfo.BillingAddress);
            insertCommand.Parameters.AddWithValue("@billing_address2", oBillingInfo.BillingAddress2);
            insertCommand.Parameters.AddWithValue("@billing_city", oBillingInfo.BillingCity);
            insertCommand.Parameters.AddWithValue("@billing_state", oBillingInfo.BillingState);
            insertCommand.Parameters.AddWithValue("@billing_zip", oBillingInfo.BillingZip);
            insertCommand.Parameters.AddWithValue("@billing_zip4", oBillingInfo.BillingZip4);
            insertCommand.Parameters.AddWithValue("@billing_country", oBillingInfo.BillingCountry);
            insertCommand.Parameters.AddWithValue("@billing_first_name", oBillingInfo.BillingFirstName);
            insertCommand.Parameters.AddWithValue("@billing_last_name", oBillingInfo.BillingLastName);
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
        public int SaveUserBillingOrder(BillingInfo oBillingInfo)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            int newid = 0;
            string insertString =
                "INSERT into tblBilling_Information (customer_id, billing_address1, billing_address2, billing_city, billing_state, billing_zip, billing_zip4, billing_country, billing_first_name, billing_last_name)" +
                " values (@customer_id, @billing_address1, @billing_address2, @billing_city, @billing_state, @billing_zip, @billing_zip4, @billing_country, @billing_first_name, @billing_last_name)SELECT SCOPE_IDENTITY()";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);
            insertCommand.Parameters.AddWithValue("@customer_id", oBillingInfo.CustomerID);
            insertCommand.Parameters.AddWithValue("@billing_address1", oBillingInfo.BillingAddress);
            insertCommand.Parameters.AddWithValue("@billing_address2", oBillingInfo.BillingAddress2);
            insertCommand.Parameters.AddWithValue("@billing_city", oBillingInfo.BillingCity);
            insertCommand.Parameters.AddWithValue("@billing_state", oBillingInfo.BillingState);
            insertCommand.Parameters.AddWithValue("@billing_zip", oBillingInfo.BillingZip);
            insertCommand.Parameters.AddWithValue("@billing_zip4", oBillingInfo.BillingZip4);
            insertCommand.Parameters.AddWithValue("@billing_country", oBillingInfo.BillingCountry);
            insertCommand.Parameters.AddWithValue("@billing_first_name", oBillingInfo.BillingFirstName);
            insertCommand.Parameters.AddWithValue("@billing_last_name", oBillingInfo.BillingLastName);
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

        public int SaveGuestInvoice(SalesInvoiceTableMetadata oInvoiceInfo)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            int newid = 0;
            string insertString =
                "INSERT into tblSales_Invoice (invoice_date, billing_id, shipping_id, invoice_email, customer_first_name, customer_last_name)" +
                " values (@invoice_date, @billing_id, @shipping_id, @invoice_email, @customer_first_name, @customer_last_name)SELECT SCOPE_IDENTITY()";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);
            //insertCommand.Parameters.AddWithValue("@customer_id", oInvoiceInfo.CustomerID);
            insertCommand.Parameters.AddWithValue("@invoice_date", oInvoiceInfo.InvoiceDate);
            insertCommand.Parameters.AddWithValue("@billing_id", oInvoiceInfo.BillingID);
            insertCommand.Parameters.AddWithValue("@shipping_id", oInvoiceInfo.ShippingID);
            insertCommand.Parameters.AddWithValue("@invoice_email", oInvoiceInfo.ShippingID);
            insertCommand.Parameters.AddWithValue("@customer_first_name", oInvoiceInfo.ShippingID);
            insertCommand.Parameters.AddWithValue("@customer_last_name", oInvoiceInfo.ShippingID);
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
        public int SaveUserInvoice(SalesInvoiceTableMetadata oInvoiceInfo)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            int newid = 0;
            string insertString =
                "INSERT into tblSales_Invoice (customer_id, invoice_date, billing_id, shipping_id, invoice_email, customer_first_name, customer_last_name)" +
                " values (@customer_id, @invoice_date, @billing_id, @shipping_id, @invoice_email, @customer_first_name, @customer_last_name)SELECT SCOPE_IDENTITY()";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);
            insertCommand.Parameters.AddWithValue("@customer_id", oInvoiceInfo.CustomerID);
            insertCommand.Parameters.AddWithValue("@invoice_date", oInvoiceInfo.InvoiceDate);
            insertCommand.Parameters.AddWithValue("@billing_id", oInvoiceInfo.BillingID);
            insertCommand.Parameters.AddWithValue("@shipping_id", oInvoiceInfo.ShippingID);
            insertCommand.Parameters.AddWithValue("@invoice_email", oInvoiceInfo.ShippingID);
            insertCommand.Parameters.AddWithValue("@customer_first_name", oInvoiceInfo.ShippingID);
            insertCommand.Parameters.AddWithValue("@customer_last_name", oInvoiceInfo.ShippingID);
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

        public bool SaveLineItem(int productId, int quantity, int invoiceId, int productOptionId)
        {
            SqlConnection oConnection = GartenkraftConnection.GetConnection();
            string insertString =
                "INSERT into tblSales_Invoice_Lineitem (product_id, lineitem_quantity, invoice_id, product_option_id)" +
                " values (@product_id, @lineitem_quantity, @invoice_id, @product_option_id)";

            SqlCommand insertCommand = new SqlCommand(insertString, oConnection);

            insertCommand.Parameters.AddWithValue("@product_id", productId);
            insertCommand.Parameters.AddWithValue("@lineitem_quantity", quantity);
            insertCommand.Parameters.AddWithValue("@invoice_id", invoiceId);
            insertCommand.Parameters.AddWithValue("@product_option_id", productOptionId);
            try
            {
                oConnection.Open();
                insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                
            }
            catch (Exception ex)
            {
                
            }

            finally
            {
                oConnection.Close();
            }
            return true;
        }
    }
}