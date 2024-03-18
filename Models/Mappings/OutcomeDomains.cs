using System.ComponentModel;

namespace PricelistGenerator.Models.Mappings;

public enum OutcomeDomains
{
    [Description("Daily Living")]
    DailyLiving,
    [Description("Home and Placement")]
    HomePlacement,
    [Description("Health and Wellbeing")]
    HealthWellbeing,
    [Description("Lifelong Learning and Education")]
    LifelongLearningEducation,
    [Description("Work and Vocation")]
    WorkVocation,
    [Description("Social and Community Participation")]
    SocialCommunityParticipation,
    [Description("Relationships, Family and Significant Others")]
    RelationshipsFamilySignificantOthers,
    [Description("Choice and Control")]
    ChoiceControl,
}