import {
  useMutation,
  UseMutationOptions,
  useQueryClient,
} from "@tanstack/react-query";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "./ui/dialog";
import { Button } from "./ui/button";
import { useState } from "react";
import Alert from "./Alert";

type ActionModalProps = {
  /** Id used for a11y */
  id: string;

  /** Title of the modal */
  title: string;

  /** Description for the modal */
  description: string;

  /** Text for the button that triggers the modal */
  buttonText: string;

  /**
   * Icon for the button that triggers the modal
   */
  buttonIcon?: React.ReactNode;

  /**


  /** Supply the tanstack query mutation options */
  mutationOptions: UseMutationOptions;

  /** Error text to display when the mutation fails */
  errorText?: string;
};

const ActionModal = ({
  title,
  description,
  id,
  buttonText,
  buttonIcon,

  mutationOptions,
  errorText,
}: ActionModalProps) => {
  const {
    mutate: mutate,
    isPending: isPending,
    isError: isError,
  } = useMutation(mutationOptions);

  const [isOpened, setIsOpened] = useState(false);

  const queryClient = useQueryClient();

  mutationOptions.onSuccess = () => {
    setIsOpened(false);
    queryClient.invalidateQueries({ queryKey: ["group"] });
  };

  const handleOnConfirm = () => {
    mutate();
  };

  const handleCancel = () => {
    setIsOpened(false);
  };

  const ActionModalTrigger = () => {
    return (
      <DialogTrigger asChild>
        <Button className="self-start" variant={"ghost"}>
          {buttonIcon} {buttonText}
        </Button>
      </DialogTrigger>
    );
  };

  return (
    <Dialog open={isOpened} onOpenChange={setIsOpened}>
      <ActionModalTrigger />
      <DialogContent aria-describedby={`${id}-dialog-title`}>
        <DialogHeader>
          <DialogTitle id={`${id}-dialog-title`}>{title}</DialogTitle>
          <DialogDescription>{description}</DialogDescription>
        </DialogHeader>
        <DialogFooter className="sm:justify-start">
          <div className="flex flex-col gap-4">
            {isError && (
              <Alert status="error" title="Error">
                {errorText ?? "Something went wrong"}
              </Alert>
            )}
            <div className="flex gap-4">
              <Button isLoading={isPending} onClick={handleOnConfirm}>
                Confirm
              </Button>
              <Button variant="destructive" onClick={handleCancel}>
                Cancel
              </Button>
            </div>
          </div>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default ActionModal;
