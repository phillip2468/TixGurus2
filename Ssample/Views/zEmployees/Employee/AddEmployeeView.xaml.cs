﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ssample.Model;

namespace Ssample.Views.zEmployees.Employee
{
    /// <summary>
    /// Interaction logic for AddEmployeeView.xaml
    /// </summary>
    public partial class AddEmployeeView : UserControl
    {
        public AddEmployeeView()
        {
            InitializeComponent(); 
            this.DataGrid.RowValidating += SfDataGrid1_RowValidating;
        }

        public void SfDataGrid1_RowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Employee_Details newRecord = args.RowData as Employee_Details;
            Employee_Details currentEmployee = new Employee_Details();

            if (newRecord != null)
            {
                currentEmployee.Employee_ID = newRecord.Employee_ID;
                currentEmployee.Email = newRecord.Email;
                currentEmployee.Password = newRecord.Password;
                currentEmployee.First_Name = newRecord.First_Name;
                currentEmployee.Last_Name = newRecord.Last_Name;
            }


            using (context)
            {
                context.Employee_Details.Add(currentEmployee);
                context.SaveChanges();
                context.Dispose();
            }

        }
    }
}
