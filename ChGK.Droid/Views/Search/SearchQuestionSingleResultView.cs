using Android.App;

namespace ChGK.Droid.Views.Search
{
    [Activity(Label = "", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class SearchQuestionSingleResultView : MenuItemIndependentView
    {
        protected override int LayoutId
        {
            get
            {
                return Resource.Layout.SearchQuestionSingleResultView;
            }
        }
    }
}