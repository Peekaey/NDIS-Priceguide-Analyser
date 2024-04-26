using PricelistGenerator.Interfaces;
using PricelistGenerator.Models;

namespace PricelistGenerator.Service;

public class PricelistAnalysisService : IPricelistAnalysisService
{
    //TODO Also check for Unit Changes,PriceLimit Changes
    // Could also potentially include registration group name/number
    // Check for Support Purchase Change by getting last character of external ID
    // Maybe also calculate percentage increases
    public PricelistAnalysisCatalog PopulateNDISSupportCatalogue(NdisSupportCatalogue oldNdisSupportCatalog,
        NdisSupportCatalogue newNdisSupportCatalogue,
        PricelistAnalysisCatalog pricelistAnalysisCatalogue)
    {
        pricelistAnalysisCatalogue = MapNDISSupportCatalogueItems(oldNdisSupportCatalog, newNdisSupportCatalogue
            , pricelistAnalysisCatalogue);


        // Mapping for all the changes
        pricelistAnalysisCatalogue = GetSupportItemsAdded(pricelistAnalysisCatalogue, newNdisSupportCatalogue);
        pricelistAnalysisCatalogue = GetSupportItemsRemoved(pricelistAnalysisCatalogue, newNdisSupportCatalogue);
        pricelistAnalysisCatalogue = GetDuplicateItemsAdded(pricelistAnalysisCatalogue, newNdisSupportCatalogue);

        pricelistAnalysisCatalogue = GetSupportItemNameChange(pricelistAnalysisCatalogue, newNdisSupportCatalogue);
        pricelistAnalysisCatalogue =
            GetSupportItemProdaSupportCategoryNameChange(pricelistAnalysisCatalogue, newNdisSupportCatalogue);
        pricelistAnalysisCatalogue =
            GetSupportItemProdaSupportCategoryNumberChange(pricelistAnalysisCatalogue, newNdisSupportCatalogue);
        pricelistAnalysisCatalogue =
            GetSupportItemPaceSupportCategoryNameChange(pricelistAnalysisCatalogue, newNdisSupportCatalogue);
        pricelistAnalysisCatalogue =
            GetSupportItemPaceSupportCategoryNumberChange(pricelistAnalysisCatalogue, newNdisSupportCatalogue);
        pricelistAnalysisCatalogue = GetPriceChanges(pricelistAnalysisCatalogue);
        pricelistAnalysisCatalogue = GetUnitChanges(pricelistAnalysisCatalogue);
        pricelistAnalysisCatalogue = GetPriceControlChanges(pricelistAnalysisCatalogue);
        pricelistAnalysisCatalogue = GetRegistrationGroupNameChange(pricelistAnalysisCatalogue);
        pricelistAnalysisCatalogue = GetRegistrationGroupNumberChange(pricelistAnalysisCatalogue);
        pricelistAnalysisCatalogue = GetSupportPurposeChange(pricelistAnalysisCatalogue);

        return pricelistAnalysisCatalogue;
    }

    private PricelistAnalysisCatalog MapNDISSupportCatalogueItems(NdisSupportCatalogue oldNdisSupportCatalog,
        NdisSupportCatalogue newNdisSupportCatalog, PricelistAnalysisCatalog pricelistAnalysisCatalogue)
    {
        pricelistAnalysisCatalogue.pricelistAnalysisCatalogSupportItems =
            new List<PricelistAnalysisCatalogSupportItem>();
        // Mapping old supportCatalogue first
        // Keeping Logic Mapping Logic Separate
        foreach (var supportItem in oldNdisSupportCatalog.NdisSupportCatalogueSupportItems)
        {
            var
                pricelistAnalysisSupportItem = new PricelistAnalysisCatalogSupportItem();

            pricelistAnalysisSupportItem.SupportItemNumber = supportItem.SupportItemNumber;
            pricelistAnalysisSupportItem.oldSupportItem = supportItem;
            pricelistAnalysisSupportItem.newSupportItem = supportItem;
            pricelistAnalysisCatalogue.pricelistAnalysisCatalogSupportItems.Add(pricelistAnalysisSupportItem);
        }

        return pricelistAnalysisCatalogue;
    }

    private PricelistAnalysisCatalog GetSupportItemsAdded(PricelistAnalysisCatalog pricelistAnalysisCatalog,
        NdisSupportCatalogue newNdisSupportCatalogue)
    {
        pricelistAnalysisCatalog.SupportItemsAdded = new List<NdisSupportCatalogueSupportItem>();
        foreach (var newSupportItem in newNdisSupportCatalogue.NdisSupportCatalogueSupportItems)
        {
            var found = false;
            foreach (var oldSupportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
                if (oldSupportItem.SupportItemNumber == newSupportItem.SupportItemNumber)
                {
                    // Means item exists in old and new pricelist - break and make copy from new pricelist
                    oldSupportItem.newSupportItem = newSupportItem;
                    found = true;
                    break;
                }

            if (!found)
                // Item exists in new pricelist but not old - new item
                pricelistAnalysisCatalog.SupportItemsAdded.Add(newSupportItem);
        }

        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetSupportItemsRemoved(PricelistAnalysisCatalog pricelistAnalysisCatalog,
        NdisSupportCatalogue newNdisSupportCatalogue)
    {
        pricelistAnalysisCatalog.SupportItemsRemoved = new List<NdisSupportCatalogueSupportItem>();
        foreach (var oldSupportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
        {
            var found = false;
            foreach (var newSupportItem in newNdisSupportCatalogue.NdisSupportCatalogueSupportItems)
                if (oldSupportItem.SupportItemNumber == newSupportItem.SupportItemNumber)
                {
                    // Means old item exists in new old and new pricelist - break and no need to take 
                    // new copy as it is already taken in GetSupportItemsAdded
                    found = true;
                    break;
                }

            if (!found)
                // Means item exists in old pricelist but not in new pricelist - removed item
                pricelistAnalysisCatalog.SupportItemsRemoved.Add(oldSupportItem.oldSupportItem);
        }

        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetDuplicateItemsAdded(PricelistAnalysisCatalog pricelistAnalysisCatalog,
        NdisSupportCatalogue newNdisSupportCatalogue)
    {
        pricelistAnalysisCatalog.DuplicateItemsAdded = new List<NdisSupportCatalogueSupportItem>();

        var duplicateItemHashSet = new HashSet<NdisSupportCatalogueSupportItem>();

        foreach (var newSupportItem in newNdisSupportCatalogue.NdisSupportCatalogueSupportItems)
            if (!duplicateItemHashSet.Add(newSupportItem))
                pricelistAnalysisCatalog.DuplicateItemsAdded.Add(newSupportItem);


        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetSupportItemNameChange(PricelistAnalysisCatalog pricelistAnalysisCatalog,
        NdisSupportCatalogue newNdisSupportCatalogue)
    {
        foreach (var pricelistAnalysisSupportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
        foreach (var newSupportItem in newNdisSupportCatalogue.NdisSupportCatalogueSupportItems)
            if (newSupportItem.SupportItemNumber == pricelistAnalysisSupportItem.SupportItemNumber)
                if (!newSupportItem.SupportItemName.Equals(pricelistAnalysisSupportItem.oldSupportItem.SupportItemName))
                    pricelistAnalysisSupportItem.NameChanged = true;
        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetSupportItemProdaSupportCategoryNameChange(
        PricelistAnalysisCatalog pricelistAnalysisCatalog
        , NdisSupportCatalogue newNdisSupportCatalogue)
    {
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
            if (supportItem.oldSupportItem.ProdaSupportCategoryName !=
                supportItem.newSupportItem.ProdaSupportCategoryName)
                supportItem.ProdaSupportCategoryNameChange = true;
        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetSupportItemPaceSupportCategoryNameChange(
        PricelistAnalysisCatalog pricelistAnalysisCatalog
        , NdisSupportCatalogue newNdisSupportCatalogue)
    {
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
            if (supportItem.oldSupportItem.PaceSupportCategoryName !=
                supportItem.newSupportItem.PaceSupportCategoryName)
                supportItem.PaceSupportCategoryNameChange = true;
        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetSupportItemProdaSupportCategoryNumberChange(
        PricelistAnalysisCatalog pricelistAnalysisCatalog
        , NdisSupportCatalogue newNdisSupportCatalogue)
    {
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
            if (supportItem.oldSupportItem.ProdaSupportCategoryNumber !=
                supportItem.newSupportItem.ProdaSupportCategoryNumber)
                supportItem.ProdaSupportCategoryNumberChanged = true;
        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetSupportItemPaceSupportCategoryNumberChange(
        PricelistAnalysisCatalog pricelistAnalysisCatalog
        , NdisSupportCatalogue newNdisSupportCatalogue)
    {
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
            if (supportItem.oldSupportItem.PaceSupportCategoryNumber !=
                supportItem.newSupportItem.PaceSupportCategoryNumber)
                supportItem.PaceSupportCategoryNumberChanged = true;
        return pricelistAnalysisCatalog;
    }

    // private PricelistAnalysisCatalog GetPriceLimitChange(PricelistAnalysisCatalog pricelistAnalysisCatalog)
    // {
    //     foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
    //     {
    //         if (supportItem.oldSupportItem.)
    //     }
    // }

    private PricelistAnalysisCatalog GetPriceChanges(PricelistAnalysisCatalog pricelistAnalysisCatalog)
    {
        pricelistAnalysisCatalog.SupportItemsWithPriceChanges =
            new List<PricelistAnalysisSupportItemsWithPriceChanges>();
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
        {
            var pricelistAnalysisSupportItemsWithPriceChanges
                = new PricelistAnalysisSupportItemsWithPriceChanges();

            var priceChange = false;
            // Stores individual values for easier manipulation/reading as well as to handle edge cases with
            // Prices for different regions increasing/decreasing
            pricelistAnalysisSupportItemsWithPriceChanges.SupportItemNumber = supportItem.SupportItemNumber;
            if (supportItem.oldSupportItem.ActPrice != supportItem.newSupportItem.ActPrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.ActPrice) >
                    decimal.Parse(supportItem.oldSupportItem.ActPrice)) {
                    supportItem.ActPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else {
                    supportItem.ActPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.ActPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewActPrice = supportItem.newSupportItem.ActPrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldActPrice = supportItem.oldSupportItem.ActPrice;

            if (supportItem.oldSupportItem.NswPrice != supportItem.newSupportItem.NswPrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.NswPrice) >
                    decimal.Parse(supportItem.oldSupportItem.NswPrice))
                {
                    supportItem.NswPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else
                {
                    supportItem.NswPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.NswPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewNswPrice = supportItem.newSupportItem.NswPrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldNswPrice = supportItem.oldSupportItem.NswPrice;

            if (supportItem.oldSupportItem.VicPrice != supportItem.newSupportItem.VicPrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.VicPrice) >
                    decimal.Parse(supportItem.oldSupportItem.VicPrice))
                {
                    supportItem.VicPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else
                {
                    supportItem.VicPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.VicPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewVicPrice = supportItem.newSupportItem.VicPrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldVicPrice = supportItem.oldSupportItem.VicPrice;

            if (supportItem.oldSupportItem.QldPrice != supportItem.newSupportItem.QldPrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.QldPrice) >
                    decimal.Parse(supportItem.oldSupportItem.QldPrice))
                {
                    supportItem.QldPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else
                {
                    supportItem.QldPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.QldPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewQldPrice = supportItem.newSupportItem.QldPrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldQldPrice = supportItem.oldSupportItem.QldPrice;

            if (supportItem.oldSupportItem.SaPrice != supportItem.newSupportItem.SaPrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.SaPrice) >
                    decimal.Parse(supportItem.oldSupportItem.SaPrice))
                {
                    supportItem.SaPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else
                {
                    supportItem.SaPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.SaPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewSaPrice = supportItem.newSupportItem.SaPrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldSaPrice = supportItem.oldSupportItem.SaPrice;

            if (supportItem.oldSupportItem.TasPrice != supportItem.newSupportItem.TasPrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.TasPrice) >
                    decimal.Parse(supportItem.oldSupportItem.TasPrice))
                {
                    supportItem.TasPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else
                {
                    supportItem.TasPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.TasPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewTasPrice = supportItem.newSupportItem.TasPrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldTasPrice = supportItem.oldSupportItem.TasPrice;

            if (supportItem.oldSupportItem.WaPrice != supportItem.newSupportItem.WaPrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.WaPrice) >
                    decimal.Parse(supportItem.oldSupportItem.WaPrice))
                {
                    supportItem.WaPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else
                {
                    supportItem.WaPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.WaPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewWaPrice = supportItem.newSupportItem.WaPrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldWaPrice = supportItem.oldSupportItem.WaPrice;

            if (supportItem.oldSupportItem.NtPrice != supportItem.newSupportItem.NtPrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.NtPrice) >
                    decimal.Parse(supportItem.oldSupportItem.NtPrice))
                {
                    supportItem.NtPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else
                {
                    supportItem.NtPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.NtPriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewNtPrice = supportItem.newSupportItem.NtPrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldNtPrice = supportItem.oldSupportItem.NtPrice;

            if (supportItem.oldSupportItem.RemotePrice != supportItem.newSupportItem.RemotePrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.RemotePrice) >
                    decimal.Parse(supportItem.oldSupportItem.RemotePrice))
                {
                    supportItem.RemotePriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else
                {
                    supportItem.RemotePriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.VeryRemotePriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewRemotePrice = supportItem.newSupportItem.RemotePrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldRemotePrice = supportItem.oldSupportItem.RemotePrice;

            if (supportItem.oldSupportItem.VeryRemotePrice != supportItem.newSupportItem.VeryRemotePrice)
            {
                if (decimal.Parse(supportItem.newSupportItem.VeryRemotePrice) >
                    decimal.Parse(supportItem.oldSupportItem.VeryRemotePrice))
                {
                    supportItem.VeryRemotePriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Increased;
                }
                else
                {
                    supportItem.VeryRemotePriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Decreased;
                }
                priceChange = true;
            }
            else
            {
                supportItem.VeryRemotePriceChange = PricelistAnalysisCatalogSupportItem.PriceChangeStatus.Unchanged;
            }

            pricelistAnalysisSupportItemsWithPriceChanges.NewVeryRemotePrice =
                supportItem.newSupportItem.VeryRemotePrice;
            pricelistAnalysisSupportItemsWithPriceChanges.OldVeryRemotePrice =
                supportItem.oldSupportItem.VeryRemotePrice;

            if (priceChange == true)
            {
                pricelistAnalysisCatalog.SupportItemsWithPriceChanges.Add(pricelistAnalysisSupportItemsWithPriceChanges);
            }

        }

        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetUnitChanges(PricelistAnalysisCatalog pricelistAnalysisCatalog)
    {
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
            if (supportItem.oldSupportItem.Unit != supportItem.newSupportItem.Unit)
                supportItem.UnitChanged = true;
        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetPriceControlChanges(PricelistAnalysisCatalog pricelistAnalysisCatalog)
    {
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
        {
            // Needs to be refactored
            var oldActPriceControl = GetPriceControl(supportItem.oldSupportItem.ActPrice);
            var oldNswPriceControl = GetPriceControl(supportItem.oldSupportItem.NswPrice);
            var oldVicPriceControl = GetPriceControl(supportItem.oldSupportItem.VicPrice);
            var oldQldPriceControl = GetPriceControl(supportItem.oldSupportItem.QldPrice);
            var oldSaPriceControl = GetPriceControl(supportItem.oldSupportItem.SaPrice);
            var oldTasPriceControl = GetPriceControl(supportItem.oldSupportItem.TasPrice);
            var oldWaPriceControl = GetPriceControl(supportItem.oldSupportItem.WaPrice);
            var oldNtPriceControl = GetPriceControl(supportItem.oldSupportItem.NtPrice);
            var oldRemotePriceControl = GetPriceControl(supportItem.oldSupportItem.RemotePrice);
            var oldVeryRemotePriceControl = GetPriceControl(supportItem.oldSupportItem.VeryRemotePrice);

            var newActPriceControl = GetPriceControl(supportItem.newSupportItem.ActPrice);
            var newNswPriceControl = GetPriceControl(supportItem.newSupportItem.NswPrice);
            var newVicPriceControl = GetPriceControl(supportItem.newSupportItem.VicPrice);
            var newQldPriceControl = GetPriceControl(supportItem.newSupportItem.QldPrice);
            var newSaPriceControl = GetPriceControl(supportItem.newSupportItem.SaPrice);
            var newTasPriceControl = GetPriceControl(supportItem.newSupportItem.TasPrice);
            var newWaPriceControl = GetPriceControl(supportItem.newSupportItem.WaPrice);
            var newNtPriceControl = GetPriceControl(supportItem.newSupportItem.NtPrice);
            var newRemotePriceControl = GetPriceControl(supportItem.newSupportItem.RemotePrice);
            var newVeryRemotePriceControl = GetPriceControl(supportItem.newSupportItem.VeryRemotePrice);


            if (oldActPriceControl != newActPriceControl) supportItem.ActPriceControlChanged = true;
            if (oldNswPriceControl != newNswPriceControl) supportItem.NswPriceControlChanged = true;
            if (oldVicPriceControl != newVicPriceControl) supportItem.VicPriceControlChanged = true;
            if (oldQldPriceControl != newQldPriceControl) supportItem.QldPriceControlChanged = true;
            if (oldSaPriceControl != newSaPriceControl) supportItem.SaPriceControlChanged = true;
            if (oldTasPriceControl != newTasPriceControl) supportItem.TasPriceControlChanged = true;
            if (oldWaPriceControl != newWaPriceControl) supportItem.WaPriceControlChanged = true;
            if (oldNtPriceControl != newNtPriceControl) supportItem.NtPriceControlChanged = true;
            if (oldRemotePriceControl != newRemotePriceControl) supportItem.RemotePriceControlChanged = true;
            if (oldVeryRemotePriceControl != newVeryRemotePriceControl)
                supportItem.VeryRemotePriceControlChanged = true;

            var pricelistAnalysisPriceControlChanges = new PricelistAnalysisPriceControlChanges();
            pricelistAnalysisPriceControlChanges.ActOldPriceControl = oldActPriceControl;
            pricelistAnalysisPriceControlChanges.ActNewPriceControl = newActPriceControl;
            pricelistAnalysisPriceControlChanges.NswOldPriceControl = oldNswPriceControl;
            pricelistAnalysisPriceControlChanges.NswNewPriceControl = newNswPriceControl;
            pricelistAnalysisPriceControlChanges.VicOldPriceControl = oldVicPriceControl;
            pricelistAnalysisPriceControlChanges.VicNewPriceControl = newVicPriceControl;
            pricelistAnalysisPriceControlChanges.QldOldPriceControl = oldQldPriceControl;
            pricelistAnalysisPriceControlChanges.QldNewPriceControl = newQldPriceControl;
            pricelistAnalysisPriceControlChanges.SaOldPriceControl = oldSaPriceControl;
            pricelistAnalysisPriceControlChanges.SaNewPriceControl = newSaPriceControl;
            pricelistAnalysisPriceControlChanges.TasOldPriceControl = oldTasPriceControl;
            pricelistAnalysisPriceControlChanges.TasNewPriceControl = newTasPriceControl;
            pricelistAnalysisPriceControlChanges.WaOldPriceControl = oldWaPriceControl;
            pricelistAnalysisPriceControlChanges.WaNewPriceControl = newWaPriceControl;
            pricelistAnalysisPriceControlChanges.NtOldPriceControl = oldNtPriceControl;
            pricelistAnalysisPriceControlChanges.NtNewPriceControl = newNtPriceControl;
            pricelistAnalysisPriceControlChanges.RemoteOldPriceControl = oldRemotePriceControl;
            pricelistAnalysisPriceControlChanges.RemoteNewPriceControl = newRemotePriceControl;
            pricelistAnalysisPriceControlChanges.VeryRemoteOldPriceControl = oldVeryRemotePriceControl;
            pricelistAnalysisPriceControlChanges.VeryRemoteNewPriceControl = newVeryRemotePriceControl;
        }

        return pricelistAnalysisCatalog;
    }


    private PricelistAnalysisCatalog GetRegistrationGroupNameChange(PricelistAnalysisCatalog pricelistAnalysisCatalog)
    {
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
            if (supportItem.oldSupportItem.RegistrationGroupName != supportItem.newSupportItem.RegistrationGroupName)
                supportItem.RegistrationGroupNameChange = true;
        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetRegistrationGroupNumberChange(PricelistAnalysisCatalog pricelistAnalysisCatalog)
    {
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
            if (supportItem.oldSupportItem.RegistrationGroupNumber !=
                supportItem.newSupportItem.RegistrationGroupNumber)
                supportItem.RegistrationGroupNumberChange = true;
        return pricelistAnalysisCatalog;
    }

    private PricelistAnalysisCatalog GetSupportPurposeChange(PricelistAnalysisCatalog pricelistAnalysisCatalog)
    {
        foreach (var supportItem in pricelistAnalysisCatalog.pricelistAnalysisCatalogSupportItems)
        {
            var oldSupportPurpose = GetSupportPurpose(supportItem.oldSupportItem.SupportItemNumber);

            var newSupportPurpose = GetSupportPurpose(supportItem.newSupportItem.SupportItemNumber);

            if (oldSupportPurpose != newSupportPurpose) supportItem.SupportPurposeChanged = true;
        }

        return pricelistAnalysisCatalog;
    }


    private string GetPriceControl(string? price)
    {
        if (price == null || price == "1") return "No Price";
        {
            return "Recommended";
        }

        return "Maximum";
    }

    private string GetSupportPurpose(string supportItem)
    {
        var identifier = supportItem.Substring(supportItem.Length - 1);
        // Uses the generic identifier to identify the support Purpose if the support item does not end with _T
        if (!supportItem.Contains("T"))
            switch (identifier)
            {
                case "1":
                    return "Core";
                case "2":
                    return "Capital";
                case "3":
                    return "Capacity Building";
                default:
                    return "Unmapped Support Purpose";
            }

        // Mapping for support items that end with _T or _D > Changes to third last character
        identifier = supportItem.Substring(supportItem.Length - 3);
        switch (identifier)
        {
            case "1_T":
                return "Core";
                break;
            case "1_D":
                return "Core";
                break;
            // Represents Identifier 1_T_D
            case "T_D":
                return "Core";
                break;
            default:
                return "Unmapped Support Purpose";
                break;
        }
    }
}