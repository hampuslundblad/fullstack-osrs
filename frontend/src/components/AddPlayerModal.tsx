import { addPlayerToGroup } from "@/api/user";
import usePlayer from "@/hooks/usePlayer";
import { useQueryClient, useMutation } from "@tanstack/react-query";
import { PlusIcon } from "lucide-react";
import { useState } from "react";
import Alert from "./Alert";
import { Button } from "./ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "./ui/dialog";
import { useParams } from "@tanstack/react-router";
import Search from "./Search";

const AddPlayerModal = ({
  isOpened,
  setIsOpened,
}: {
  isOpened: boolean;
  setIsOpened: (open: boolean) => void;
}) => {
  const { groupName } = useParams({ strict: false });

  if (!groupName) {
    throw new Error("Group name is required");
  }

  const queryClient = useQueryClient();

  const [playerName, setPlayerName] = useState("");

  const {
    data: playerData,
    isLoading: isPlayerLoading,
    isError: isPlayerError,
  } = usePlayer(playerName);

  const {
    mutate: addPlayer,
    isPending: isAddPlayerLoading,
    isError: isAddPlayerError,
  } = useMutation({
    mutationFn: (playerName: string) => addPlayerToGroup(groupName, playerName),
    mutationKey: ["addPlayer", groupName, playerName],
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["group", groupName] });
      setIsOpened(false);
    },
  });

  const handleOnAddPlayerClick = () => {
    addPlayer(playerName);
  };

  const AddPlayerButton = () => {
    return (
      <DialogTrigger asChild className="">
        <Button>
          <PlusIcon /> Add player
        </Button>
      </DialogTrigger>
    );
  };

  return (
    <Dialog open={isOpened} onOpenChange={setIsOpened}>
      <AddPlayerButton />
      <DialogContent aria-describedby="add-player-dialog-title">
        <DialogHeader>
          <DialogTitle id="add-player-dialog-title">Add a player</DialogTitle>
          <DialogDescription>
            Add a player by searching for their RSN
          </DialogDescription>
        </DialogHeader>

        <div className="flex flex-col gap-12 my-8">
          <Search
            isLoading={isPlayerLoading}
            id={"add-player-search"}
            label={"Player name"}
            onSearch={(value) => setPlayerName(value)}
          />
          {!isPlayerLoading && !isPlayerError && playerData && (
            <div className="flex flex-col gap-4 ">
              <div>
                <p>Player name : {playerData.name}</p>
                <p>Total Experience : {playerData.totalExperience}</p>
                <p>
                  Experience gained last week :{" "}
                  {playerData.experienceGainedLastWeek}
                </p>
              </div>
              {isAddPlayerError && (
                <Alert
                  status="error"
                  title="There was an error adding the player"
                />
              )}
              <Button
                variant={"secondary"}
                className="self-start"
                onClick={handleOnAddPlayerClick}
                isLoading={isAddPlayerLoading}
              >
                Add Player
              </Button>
            </div>
          )}
        </div>

        <DialogFooter className="sm:justify-start">
          {/* <Button onClick={handleOnClick}>Add player</Button> */}
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};
export default AddPlayerModal;
