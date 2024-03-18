using System.ComponentModel;

namespace PricelistGenerator.Models.Mappings;

public enum SupportCategory
{
    [Description("Assistance With Daily Living")]
    AssistanceWithDailyLiving,
    [Description("Transport")]
    Transport,
    [Description("Consumables")]
    Consumables,
    [Description("Assistance With Social And Community Participation")]
    AssistanceWithSocialAndCommunityParticipation,
    [Description("Assistive Technology")]
    AssistiveTechnology,
    [Description("Home Modifications")]
    HomeModifications,
    [Description("Support Coordination")]
    SupportCoordination,
    [Description("Improved Living Arrangements")]
    ImprovedLivingArrangements,
    [Description("Increased Social And Community Participation")]
    IncreasedSocialAndCommunityParticipation,
    [Description("Finding And Keeping A Job")]
    FindingAndKeepingAJob,
    [Description("Improved Relationships")]
    ImprovedRelationships,
    [Description("Improved Health And Wellbeing")]
    ImprovedHealthAndWellbeing,
    [Description("Improved Learning")]
    ImprovedLearning,
    [Description("Improved Life Choices")]
    ImprovedLifeChoices,
    [Description("Improved Daily Living Skills")]
    ImprovedDailyLivingSkills
    
}