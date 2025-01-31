import { fetchGroup } from "@/api/user";
import AddPlayerModal from "@/components/AddPlayerModal";
import Alert from "@/components/Alert";
import Layout from "@/components/Layout";
import PlayerCard from "@/components/PlayerCard";
import { Button } from "@/components/ui/button";
import { queryOptions, useQuery } from "@tanstack/react-query";
import { createFileRoute } from "@tanstack/react-router";
import { TrashIcon } from "lucide-react";
import { useState } from "react";

const groupQueryOptions = (groupName: string) =>
  queryOptions({
    queryKey: ["group", groupName],
    queryFn: () => fetchGroup(groupName),
  });

export const Route = createFileRoute("/_auth/mygroups/$groupName")({
  loader: async ({ params, context }) => {
    return context.queryClient.ensureQueryData(
      groupQueryOptions(params.groupName)
    );
  },
  staleTime: 10 * 60 * 1000, // 10 minutes
  component: RouteComponent,
  errorComponent: () => <div>Error</div>,
  pendingComponent: () => <div>Loading</div>,
});

function RouteComponent() {
  const { groupName } = Route.useParams();
  const { data: groupData } = useQuery(groupQueryOptions(groupName));

  const [isPlayerModalOpen, setIsAddPlayerModalOpen] = useState(false);

  // Will not be undefined since we're loading it when the user navigates here.
  const players = groupData?.players;

  return (
    <Layout title={groupName} showBackButton>
      <div className="flex flex-col gap-8 mt-8">
        <div className="flex gap-8">
          <AddPlayerModal
            isOpened={isPlayerModalOpen}
            setIsOpened={setIsAddPlayerModalOpen}
          />
          <Button variant={"destructive"}>
            Delete Group <TrashIcon />
          </Button>
        </div>
        <div className="mt-8 flex flex-col gap-8">
          {players && players.map((player) => <PlayerCard player={player} />)}
          {players && players.length === 0 && (
            <Alert
              className="w-1/2"
              title="No players"
              description="Add players using the Add Player button"
            />
          )}
        </div>
      </div>
    </Layout>
  );
}
