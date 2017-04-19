using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace DivisasRestFull
{
	public partial class HomePage : ContentPage
	{

		private Conexion conexion;

		public HomePage()
		{
			InitializeComponent();
			inicialList();
			this.LoadRates();
			convertButton.Clicked += convertButton_Clicked;
		}


		private async void convertButton_Clicked(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(amoutEntry.Text))
			{
				await DisplayAlert("Error", "Debe ingresar una cantidad mayor a 0", "Aceptar");
				amoutEntry.Focus();
				return;
			}

			var amout = Decimal.Parse(amoutEntry.Text);
			if (amout <= 0)
			{
				await DisplayAlert("Error", "Debe ingresar una cantidad mayor a 0", "Aceptar");
				amoutEntry.Focus();
				return;
			}

			if (sourcePicker.SelectedIndex == -1)
			{
				await DisplayAlert("Error", "Debe ingresar una moneda origen", "Aceptar");
				sourcePicker.Focus();
				return;
			}
			if (targetPicker.SelectedIndex == -1)
			{
				await DisplayAlert("Error", "Debe ingresar una moneda destino", "Aceptar");
				targetPicker.Focus();
				return;
			}

			var amoutConvert = this.Convert(amout, sourcePicker.SelectedIndex, targetPicker.SelectedIndex);

			convertedlabel.Text = string.Format(
				"{0:N2} {1} = {2:N2} {3}",
				amout,
				sourcePicker.Items[sourcePicker.SelectedIndex],
				amoutConvert,
				targetPicker.Items[targetPicker.SelectedIndex]
			);

		}

		private decimal Convert(decimal amout, int source, int target)
		{
			double rateResource = this.GetRate(source);
			double rateTarget = this.GetRate(target);
			return amout / (decimal)rateResource * (decimal)rateTarget;
		}

		private double GetRate(int index)
		{
			switch (index)
			{
				case 0: return conexion.Rates.DKK;
				case 1: return conexion.Rates.CAD;
				case 2: return conexion.Rates.USD;
				case 3: return conexion.Rates.UR;
				case 4: return conexion.Rates.CH;
				case 5: return conexion.Rate.GBP;
				case 6: return conexion.RatesCLP;
				case 7: return conexion.Rates.OP;
				case 8: return conexion.Rates.MN;
				case 9: return conexion.Rates.BR;
				case 10: return conexion.Rates.INR;
				case 11: return conexion.Rates.JPY;
				default: return 1;
			}
		}

		public async void LoadRates()
		{
			waitActivityIndicator.IsRunning = true;
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("https://openexchangerates.org/");
			string url = string.Format("api/latest.json?app_id=fa4e1fd1954c4563813706bcacf32db0");
			var response = await client.GetAsync(url);
			var result = response.Content.ReadAsStringAsync().Result;
			waitActivityIndicator.IsRunning = false;

			if (string.IsNullOrEmpty(result))
			{
				await DisplayAlert("Error", "Sin conexion al rervidor", "Aceptar");
				return;
			}
			this.conexion = JsonConvert.DeserializeObject<Conexion>(result);
		}

		private void inicialList()
		{
			sourcePicker.Items.Add("Coronas Danesas");
			sourcePicker.Items.Add("Dolares Canadienses");
			sourcePicker.Items.Add("Dolares Estadunidenses");
			sourcePicker.Items.Add("Euros");
			sourcePicker.Items.Add("Francos Suizos");
			sourcePicker.Items.Add("Libras Esterlinas");
			sourcePicker.Items.Add("Pesos Chilenos");
			sourcePicker.Items.Add("Pesos Colombianos");
			sourcePicker.Items.Add("Pesos Mexicanos");
			sourcePicker.Items.Add("Reales Basileros");
			sourcePicker.Items.Add("Rupias Indias");
			sourcePicker.Items.Add("Yenes Japoneses");


			targetPicker.Items.Add("Coronas Danesas");
			targetPicker.Items.Add("Dolares Canadienses");
			targetPicker.Items.Add("Dolares Estadunidenses");
			targetPicker.Items.Add("Euros");
			targetPicker.Items.Add("Francos Suizos");
			targetPicker.Items.Add("Libras Esterlinas");
			targetPicker.Items.Add("Pesos Chilenos");
			targetPicker.Items.Add("Pesos Colombianos");
			targetPicker.Items.Add("Pesos Mexicanos");
			targetPicker.Items.Add("Reales Basileros");
			targetPicker.Items.Add("Rupias Indias");
			targetPicker.Items.Add("Yenes Japoneses");
		}


	}
}
