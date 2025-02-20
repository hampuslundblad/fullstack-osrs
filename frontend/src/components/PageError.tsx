import { RefreshCcw } from "lucide-react";
import { Button } from "./ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";

const PageError = () => {
  return (
    <div className="mx-auto mt-24">
      <Card className="p-8">
        <CardHeader>
          <CardTitle>Oops! There seems like something went wrong </CardTitle>
        </CardHeader>
        <CardContent className="flex flex-col gap-4">
          <p>
            An error has occured, please try again later. You can try reloading
            the page and see if that helps.
          </p>
          <Button onClick={() => window.location.reload()}>
            <RefreshCcw /> Reload
          </Button>
        </CardContent>
      </Card>
    </div>
  );
};

export default PageError;
