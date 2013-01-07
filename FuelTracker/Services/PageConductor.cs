using System;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Messages;
using FuelTracker.Views;
using FuelTracker.Web;
using System.Windows;

namespace FuelTracker.Services
{
    public class PageConductor : IPageConductor
    {
        protected Frame RootFrame { get; set; }

        public PageConductor()
        {
            RegisterMessages();
        }

        public void RegisterMessages()
        {
            RegisterDialogMessages();

            //RegisterModelTypeMessages();
        }


        #region DialogMessages
        public void DisplayError(string origin, Exception e, string details)
        {
            string description = string.Format("Error occured in {0}. {1} {2}", origin, details, e.Message);
            var error = new Error()
            {
                Message = description,
                Title = "Error Occurred"
            };

            ErrorMessage errorMessage = new ErrorMessage(error);
            Messenger.Default.Send(errorMessage);
        }


        public void DisplayError(string origin, Exception e)
        {
            DisplayError(origin, e, string.Empty);
        }


        private void RegisterDialogMessages()
        {
            Messenger.Default.Register<SuccessDialogMessage>(this, OnSuccessDialogMessage);
            Messenger.Default.Register<ErrorDialogMessage>(this, OnErrorDialogMessage);
        }

        private void OnSuccessDialogMessage(SuccessDialogMessage msg)
        {
            MessageBox.Show(msg.Content, msg.Caption, msg.Button);
        }

        private void OnErrorDialogMessage(ErrorDialogMessage msg)
        {
            MessageBox.Show(msg.Content, msg.Caption, msg.Button);
        }

        #endregion

        #region Navigation
        public void GoToView(string viewToken)
        {
            Go(FormatViewPath(viewToken));
        }

        public void GoBack()
        {
            RootFrame.GoBack();
        }

        private void Go(string path)
        {
            RootFrame.Navigate(new Uri(path, UriKind.Relative));
        }

        private string FormatViewPath(string viewToken)
        {
            return string.Format("/{0}/{1}.xaml", ViewTokens.Root, viewToken);
        }
        #endregion


        //#region Recipe Messages
        
        //protected void RegisterRecipeMessages()
        //{
        //    Messenger.Default.Register<LaunchEditRecipeMessage>(this, OnLaunchEditRecipeMessage);
        //    Messenger.Default.Register<LaunchNewRecipeMessage>(this, OnLaunchNewRecipeMessage);            
        //}

        //protected void OnLaunchEditRecipeMessage(LaunchEditRecipeMessage msg)
        //{
        //    var recipe = msg.Recipe;
        //    int recipeId = recipe.RecipeId;
        //    RecipeEditViewWindow recipeEditViewWindow = new RecipeEditViewWindow(recipeId);
        //    recipeEditViewWindow.Show();
        //}

        //protected void OnLaunchNewRecipeMessage(LaunchNewRecipeMessage msg)
        //{
        //    int newRecipeId = 0;//It must be 0 to create new
        //    RecipeEditViewWindow recipeEditViewWindow = new RecipeEditViewWindow(newRecipeId);
        //    recipeEditViewWindow.Show();
        //}

        //#endregion

        #region Car Messages
        public void RegisterCarMessages()
        {
            Messenger.Default.Register<FuelTracker.Messages.LaunchNewCarMessage>(this, OnLaunchNewCarMessage);
        }

        private void OnLaunchNewCarMessage(FuelTracker.Messages.LaunchNewCarMessage msg)
        {
            //Most frequent use : Show Edit window for Car
            Car car = msg.Car;
            ShowEditCarWindow(car);
        }

        private void ShowEditCarWindow(Car car)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}