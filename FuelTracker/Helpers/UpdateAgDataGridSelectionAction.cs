//------------------------------------------------------------------------------
// <copyright file="UpdateAgDataGridSelectionAction.cs" company="DNS Technology Pty Ltd.">
//   Copyright (c) 2009 DNS Technology Pty Ltd. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace FuelTracker
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Interactivity;
    using DevExpress.AgDataGrid;

    /// <summary>
    /// Maintains a target collection with the currently selected items 
    /// of the data grid that the action is attached to.
    /// </summary>
    public class UpdateAgDataGridSelectionAction : TriggerAction<AgDataGrid>
    {
        /// <summary>The DependencyProperty backing store for SelectionTargetPath.  This enables animation, styling, binding, etc...</summary>
        public static readonly DependencyProperty SelectionTargetPathProperty =
            DependencyProperty.Register("SelectionTargetPath", typeof(string), typeof(UpdateAgDataGridSelectionAction), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the path to the selection target to bind to
        /// </summary>
        public string SelectionTargetPath
        {
            get
            {
                return (string)this.GetValue(SelectionTargetPathProperty);
            }

            set
            {
                this.SetValue(SelectionTargetPathProperty, value);
            }
        }

        /// <summary>
        /// Updates the target collection with the currently selected items.
        /// </summary>
        /// <param name="parameter">The selection event args from the DataGrid</param>
		protected override void Invoke(object parameter)
		{
			var focusedObject = this.AssociatedObject.FocusedDataRow;

			var prop = this.AssociatedObject.DataContext.GetType().GetProperty(this.SelectionTargetPath);
			var list = prop.GetValue(this.AssociatedObject.DataContext, null) as IList;
			if (list == null)
			{
				return;
			}

			list.Clear();

			list.Add(focusedObject);
		}
    }
}
