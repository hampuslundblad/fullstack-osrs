import {  PlayerExperience } from "@/api/user";

type TimeRange = "1D" | "1W" | "1M" | "1Y";

export const experienceGainedOverTime = (playerExperiences: PlayerExperience[], timeRange: TimeRange = "1M") =>  {
    if(playerExperiences.length === 0) {
        return 0;
    }

    const dateTimeRange = timeRangeToDate(timeRange);
    
    const experienceGainedOverTimeRange = playerExperiences.filter(
        (experienceEntry) => new Date(experienceEntry.dateTime) >= dateTimeRange
      );

      if(experienceGainedOverTimeRange.length <= 1) {
          return 0;
      }
      // Sort the experience entries by experience in ascending order, largest first.
      experienceGainedOverTimeRange.sort((a, b) => {
            return a.experience - b.experience;
        });

    return experienceGainedOverTimeRange[experienceGainedOverTimeRange.length - 1].experience - experienceGainedOverTimeRange[0].experience;

}

const timeRangeToDate = (range: TimeRange) => {
    const date = new Date();
    switch (range) {
        case "1D":
            date.setDate(date.getDate() - 1);
            break;
        case "1W":
            date.setDate(date.getDate() - 7);
            break;
        case "1M":
            date.setMonth(date.getMonth() - 1);
            break;
        case "1Y":
            date.setFullYear(date.getFullYear() - 1);
            break;
    }
    return date;
}