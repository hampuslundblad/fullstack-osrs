import { Player, removePlayerFromGroup } from "@/api/user";
import { TrashIcon } from "lucide-react";
import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";
import { useParams } from "@tanstack/react-router";
import { UseMutationOptions } from "@tanstack/react-query";
import ActionModal from "./ActionModal";
import { experienceGainedOverTime } from "@/utils/playerUtils";

const PlayerCard = ({ player }: { player: Player }) => {
  const experienceOverTime = player.experienceOverTime ?? [];

  return (
    <Card className="w-96">
      <CardHeader>
        <div className="flex justify-between">
          <CardTitle>{player.playerName}</CardTitle>

          <RemovePlayerModal playerName={player.playerName} />
        </div>
      </CardHeader>
      <CardContent>
        <div>
          <p>Total level : {player.totalLevel}</p>
          <p>
            Experience gained last week:{" "}
            {experienceGainedOverTime(experienceOverTime, "1W")}
          </p>
        </div>
      </CardContent>
    </Card>
  );
};

const RemovePlayerModal = ({ playerName }: { playerName: string }) => {
  const { groupName } = useParams({ strict: false });
  if (groupName === undefined) {
    throw new Error("Group name cannot be undefined");
  }

  const removePlayerMutateOptions: UseMutationOptions = {
    mutationFn: () => removePlayerFromGroup(groupName, playerName),
  };

  return (
    <ActionModal
      id={"remove-player"}
      title={"Remove player"}
      description={"Do you want to remove this player?"}
      buttonText={""}
      buttonIcon={<TrashIcon />}
      mutationOptions={removePlayerMutateOptions}
    />
  );
};

export default PlayerCard;
