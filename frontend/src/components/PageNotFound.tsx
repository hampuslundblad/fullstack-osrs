import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";

const PageNotFound = () => {
  return (
    <div className="mx-auto mt-24">
      <Card className="p-8">
        <CardHeader>
          <CardTitle>
            404: We were unable to find the page that you're looking for.
          </CardTitle>
        </CardHeader>
        <CardContent className="">
          <p>
            {" "}
            Most likely there's a typo in the url or the page has been moved to
            another route.{" "}
          </p>
        </CardContent>
      </Card>
    </div>
  );
};

export default PageNotFound;
