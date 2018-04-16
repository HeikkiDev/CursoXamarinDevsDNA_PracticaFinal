using AppCursoXamarinDevsDNA.Base;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using System.Threading.Tasks;

namespace AppCursoXamarinDevsDNA.Features.DetailPage
{
    public class DetailPageViewModel : BaseViewModel
    {
        private string _labelText = "";
        public string LabelText
        {
            get { return _labelText; }
            set { this.RaiseAndSetIfChanged(ref _labelText, value); }
        }

        public DetailPageViewModel()
        {

        }

        public override void Load(NavigationParameters navigationParameters)
        {
            base.Load(navigationParameters);

            if (navigationParameters == null) return;

            if (navigationParameters.ContainsKey("detail-text"))
            {
                LabelText = navigationParameters["detail-text"].ToString();
            }
        }
    }
}
