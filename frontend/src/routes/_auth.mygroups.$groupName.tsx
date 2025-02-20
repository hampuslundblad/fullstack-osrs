import { fetchGroup, Player, syncGroup } from "@/api/user";
import AddPlayerModal from "@/components/AddPlayerModal";
import Alert from "@/components/Alert";
import Layout from "@/components/Layout";
import PlayerCard from "@/components/PlayerCard";
import { Button } from "@/components/ui/button";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { useToast } from "@/hooks/useToast";
import { experienceGainedOverTime } from "@/utils/playerUtils";
import {
  queryOptions,
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";
import { createFileRoute } from "@tanstack/react-router";
import { RefreshCwIcon, TrashIcon } from "lucide-react";
import { useState } from "react";

const groupQueryOptions = (groupName: string) =>
  queryOptions({
    queryKey: ["group", groupName],
    queryFn: () => fetchGroup(groupName),
    staleTime: 10 * 60 * 1000, // 10 minutes
  });

export const Route = createFileRoute("/_auth/mygroups/$groupName")({
  loader: async ({ params, context }) => {
    return context.queryClient.ensureQueryData(
      groupQueryOptions(params.groupName)
    );
  },
  component: RouteComponent,
  pendingComponent: () => <div>Loading</div>,
});

function RouteComponent() {
  const { groupName } = Route.useParams();
  const { data: groupData } = useQuery(groupQueryOptions(groupName));

  const [isPlayerModalOpen, setIsAddPlayerModalOpen] = useState(false);

  // Players will not be undefined since we're loading it when the user navigates here.
  // Sort in descending order of experience gained in the last month
  const players = groupData?.players.sort((a, b) => {
    return (
      experienceGainedOverTime(b.experienceOverTime ?? [], "1M") -
      experienceGainedOverTime(a.experienceOverTime ?? [], "1M")
    );
  });

  return (
    <Layout title={groupName} showBackButton>
      <div className="flex flex-col gap-8 mt-8">
        <div className="flex gap-8">
          <AddPlayerModal
            isOpened={isPlayerModalOpen}
            setIsOpened={setIsAddPlayerModalOpen}
          />

          <SyncGroupButton />

          <Button variant={"destructive"}>
            Delete Group <TrashIcon />
          </Button>
        </div>
        <Tabs defaultValue="experience" className="w-1/2">
          <TabsList className="px-4">
            <TabsTrigger className="mx-4" value="experience">
              Experience
            </TabsTrigger>
            <TabsTrigger className="mx-4" value="bosses">
              Bosses
            </TabsTrigger>
            <TabsTrigger className="mx-4" value="clue-scrolls">
              Clue scrolls
            </TabsTrigger>
          </TabsList>
          <TabsContent value="experience">
            <Players players={players} />
          </TabsContent>
          <TabsContent value="bosses">
            <Players players={players} />
          </TabsContent>
          <TabsContent value="clue-scrolls">
            <Players players={players} />
          </TabsContent>
        </Tabs>
        {/** Display players */}
      </div>
    </Layout>
  );
}

const Players = ({ players }: { players: Player[] | undefined }) => {
  return (
    <div className="mt-8 flex flex-col gap-8">
      {players &&
        players.map((player, index) => (
          <PlayerCard
            key={"playercard" + player.playerName + index}
            player={player}
          />
        ))}
      {players && players.length === 0 && (
        <Alert
          className="w-1/2"
          title="No players"
          description="Add players using the Add Player button"
        />
      )}
    </div>
  );
};

const SyncGroupButton = () => {
  const { groupName } = Route.useParams();

  const queryClient = useQueryClient();

  const { toast } = useToast();

  const { mutate: sendSyncGroup, isPending } = useMutation({
    mutationFn: () => syncGroup(groupName),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["group", groupName] });
      toast({
        title: "Group synced!",
        variant: "success",
      });
    },
    onError: () => {
      toast({
        title: "Failed to sync group.",
        description:
          "This might be due to OSRS API being difficult. Try again in a few minutes",
        variant: "destructive",
      });
    },
  });
  return (
    <Button
      variant="secondary"
      onClick={() => sendSyncGroup()}
      isLoading={isPending}
    >
      Sync group
      {!isPending && <RefreshCwIcon />}
    </Button>
  );
};
