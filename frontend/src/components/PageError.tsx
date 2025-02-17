import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";

const PageError = () => {
  return (
    <div className="mx-auto mt-24">
      <Card className="p-8">
        <CardHeader>
          <CardTitle>Oops! There seems like something went wrong </CardTitle>
        </CardHeader>
        <CardContent className="">
          <p> An error has occured, please try again later</p>
        </CardContent>
      </Card>
    </div>
  );
};

export default PageError;
