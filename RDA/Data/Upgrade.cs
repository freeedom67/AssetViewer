﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RDA.Data {

  public class Upgrade {

    #region Properties
    public Icon Icon { get; set; }
    public Description Text { get; set; }
    public String Value { get; set; }
    public List<AdditionalOutput> AdditionalOutputs { get; set; }
    public List<ReplaceInput> ReplaceInputs { get; set; }
    public List<InputAmountUpgrade> InputAmountUpgrades { get; set; }
    public Description ReplacingWorkforce { get; set; }
    #endregion

    #region Constructor
    public Upgrade(XElement element) {
      var isPercent = element.Element("Percental") == null ? false : element.Element("Percental").Value == "1";
      var value = element.Element("Value") == null ? null : (Int32?)Int32.Parse(element.Element("Value").Value);
      var factor = 1;
      switch (element.Name.LocalName) {
        case "ProductivityUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_options.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "AdditionalOutput":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_plus.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          this.AdditionalOutputs = new List<AdditionalOutput>();
          foreach (var item in element.Elements()) {
            this.AdditionalOutputs.Add(new AdditionalOutput(item));
          }
          break;
        case "ReplaceInputs":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_traderoutes.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          this.ReplaceInputs = new List<ReplaceInput>();
          foreach (var item in element.Elements()) {
            this.ReplaceInputs.Add(new ReplaceInput(item));
          }
          break;
        case "InputAmountUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_options.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          this.InputAmountUpgrades = new List<InputAmountUpgrade>();
          foreach (var item in element.Elements()) {
            this.InputAmountUpgrades.Add(new InputAmountUpgrade(item));
          }
          break;
        case "OutputAmountFactorUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_options.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "NeededAreaPercentUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/3dicons/icon_general_module_01.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          isPercent = true;
          break;
        case "AddedFertility":
          var addedFertility = Program.Original.Root.XPathSelectElement($"//Asset/Values/Standard[GUID={element.Value}]");
          this.Icon = new Icon(addedFertility.Element("IconFilename").Value);
          this.Text = new Description("Provided", "bereitgestellt");
          break;
        case "NeedsElectricity":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_electricity.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "AttractivenessUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_attractiveness.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "MaintenanceUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_credits.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "WorkforceAmountUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_options.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "ReplacingWorkforce":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_build_menu.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          var en = "Instead of its usual workforce, the building employs";
          var de = "Statt der üblichen Arbeitskräfte beschäftigt das Gebäude";
          this.ReplacingWorkforce = new Description($"{en} {Program.DescriptionEN[element.Value]}", $"{de} {Program.DescriptionDE[element.Value]}");
          break;
        case "ModuleLimitUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/3dicons/icon_general_module_01.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "AdditionalHappiness":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_happy.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "ResidentsUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_kontor_2d.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "StressUpgrade":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_incident_riot.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        case "ProvideElectricity":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_electricity.png");
          this.Text = new Description(Helper.GetDescriptionID(element.Name.LocalName));
          break;
        default:
          throw new NotImplementedException();
      }
      if (value == null) {
        this.Value = String.Empty;
      } else {
        if (isPercent) {
          this.Value = value > 0 ? $"+{value * factor}%" : $"{value * factor}%";
        } else {
          this.Value = value > 0 ? $"+{value * factor}" : $"{value * factor}";
        }
      }
    }
    public Upgrade(String key, String amount) {
      var value = amount == null ? null : (Int32?)Int32.Parse(amount);
      switch (key) {
        case "Hunting":
          this.Icon = new Icon("data/ui/2kimages/main/icons/ship_info/icon_damage.png");
          this.Text = new Description(Helper.GetDescriptionID(key));
          break;
        case "Navigation":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_threat_navigation_tint.png");
          this.Text = new Description(Helper.GetDescriptionID(key));
          break;
        case "Crafting":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_build_menu.png");
          this.Text = new Description(Helper.GetDescriptionID(key));
          break;
        case "Might":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_threat_navigation_tint.png");
          this.Text = new Description(Helper.GetDescriptionID("Might"));
          break;
        case "Melee":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_threat_melee_tint.png");
          this.Text = new Description(Helper.GetDescriptionID("Might"));
          break;
        case "Diplomacy":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_diplomacy_option_negotiate.png");
          this.Text = new Description(Helper.GetDescriptionID(key));
          break;
        case "Faith":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_church_2d.png");
          this.Text = new Description(Helper.GetDescriptionID(key));
          break;
        case "Medicine":
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_threat_diseases_tint.png");
          this.Text = new Description(Helper.GetDescriptionID(key));
          break;
        case "PerkFemale":
        case "PerkMale":
          value = null;
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_expedition_anthropologist.png");
          this.Text = new Description(Helper.GetDescriptionID(key)).InsertBefore("Trait: ", "Merkmal: ");
          break;
        case "PerkFormerPirate":
          value = null;
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_expedition_pirate.png");
          this.Text = new Description(Helper.GetDescriptionID(key)).InsertBefore("Trait: ", "Merkmal: ");
          break;
        case "PerkDiver":
          value = null;
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_expedition_diver.png");
          this.Text = new Description(Helper.GetDescriptionID(key)).InsertBefore("Trait: ", "Merkmal: ");
          break;
        case "PerkZoologist":
          value = null;
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_expedition_zoologist.png");
          this.Text = new Description(Helper.GetDescriptionID(key)).InsertBefore("Trait: ", "Merkmal: ");
          break;
        case "PerkMilitaryShip":
          value = null;
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_expedition_goto.png");
          this.Text = new Description(Helper.GetDescriptionID(key)).InsertBefore("Trait: ", "Merkmal: ");
          break;
        case "PerkHypnotist":
          value = null;
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_expedition_hypnotist.png");
          this.Text = new Description(Helper.GetDescriptionID(key)).InsertBefore("Trait: ", "Merkmal: ");
          break;
        case "PerkAnthropologist":
          value = null;
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_expedition_anthropologist.png");
          this.Text = new Description(Helper.GetDescriptionID(key)).InsertBefore("Trait: ", "Merkmal: ");
          break;
        case "PerkPolyglot":
          value = null;
          this.Icon = new Icon("data/ui/2kimages/main/icons/icon_generic_expedition.png");
          this.Text = new Description(Helper.GetDescriptionID(key)).InsertBefore("Trait: ", "Merkmal: ");
          break;
        default:
          throw new NotImplementedException();
      }
      if (value == null) {
        this.Value = String.Empty;
      } else {
        this.Value = value > 0 ? $"+{value}" : $"{value}";
      }
    }
    #endregion

    #region Public Methods
    public XElement ToXml() {
      var result = new XElement(this.GetType().Name);
      result.Add(this.Icon.ToXml());
      result.Add(this.Text.ToXml("Text"));
      result.Add(new XElement("Value", this.Value));
      if (this.AdditionalOutputs != null) result.Add(new XElement("AdditionalOutputs", this.AdditionalOutputs.Select(s => s.ToXml())));
      if (this.ReplaceInputs != null) result.Add(new XElement("ReplaceInputs", this.ReplaceInputs.Select(s => s.ToXml())));
      if (this.InputAmountUpgrades != null) result.Add(new XElement("InputAmountUpgrades", this.InputAmountUpgrades.Select(s => s.ToXml())));
      if (this.ReplacingWorkforce != null) result.Add(this.ReplacingWorkforce.ToXml("ReplacingWorkforce"));
      return result;
    }
    #endregion

  }

}