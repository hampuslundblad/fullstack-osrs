import { createGroupOnUser } from "@/api/user";
import Alert from "@/components/Alert";
import GroupCard from "@/components/GroupCard";
import Layout from "@/components/Layout";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useToast } from "@/hooks/useToast";
import { useMutation } from "@tanstack/react-query";
import {
  createFileRoute,
  getRouteApi,
  useRouter,
} from "@tanstack/react-router";
import { PlusIcon } from "lucide-react";
import { useRef, useState } from "react";

export const Route = createFileRoute("/_auth/mygroups/")({
  component: RouteComponent,
});

function RouteComponent() {
  const routeApi = getRouteApi("/_auth");
  const userData = routeApi.useLoaderData();

  return (
    <Layout title="My groups">
      <div className="mt-8 flex flex-col gap-8">
        <CreateGroupDialog />
        <div className="flex gap-4 flex-wrap">
          {userData.groups.map((group, index) => (
            <GroupCard key={group.groupName + index} group={group} />
          ))}
        </div>
      </div>
    </Layout>
  );
}

const AddGroupButton = () => {
  return (
    <DialogTrigger className="self-start border-2 p-2 rounded-full transition ease-in-out delay-150 hover:border-primary hover:scale-110">
      <PlusIcon />
    </DialogTrigger>
  );
};

const CreateGroupDialog = () => {
  const groupNameRef = useRef<HTMLInputElement>(null);

  const [isOpened, setIsOpened] = useState(false);

  const { toast } = useToast();

  const router = useRouter();

  const {
    mutate: createGroup,
    isError: isCreateGroupError,
    isPending: isCreateGroupPending,
  } = useMutation({
    mutationFn: (groupName: string) => createGroupOnUser(groupName),
    onSuccess: () => {
      setIsOpened(false);
      toast({
        title: `${groupNameRef.current?.value ?? "Group"} created!`,
        variant: "success",
      });
      router.invalidate(); // Invalidate the loader to refetch the data
    },
  });

  const handleOnClick = () => {
    createGroup(groupNameRef.current?.value ?? "");
  };

  const handleOnKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") {
      createGroup(groupNameRef.current?.value ?? "");
    }
  };

  return (
    <Dialog open={isOpened} onOpenChange={setIsOpened}>
      <AddGroupButton />
      <DialogContent aria-describedby="dialog-title">
        <DialogHeader>
          <DialogTitle id="dialog-title">Create a group</DialogTitle>
        </DialogHeader>
        <div className="flex flex-col gap-4 my-4">
          <Label htmlFor="create-group-input">Name of group</Label>
          <Input
            disabled={isCreateGroupPending}
            ref={groupNameRef}
            id="create-group-input"
            onKeyDown={handleOnKeyDown}
          />
          {isCreateGroupError && (
            <Alert
              status="error"
              title="There was an error creating the group"
            />
          )}
        </div>
        <DialogFooter className="sm:justify-start">
          <Button isLoading={isCreateGroupPending} onClick={handleOnClick}>
            Create group
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};
