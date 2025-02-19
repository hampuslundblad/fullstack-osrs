import { Group } from "@/api/user";

import { Card, CardHeader, CardTitle, CardContent } from "./ui/card";
import { Link } from "@tanstack/react-router";

type GroupCardProps = {
  group: Group;
};

const GroupCard = ({ group }: GroupCardProps) => {
  return (
    <Link
      to={"/mygroups/$groupName"}
      params={{ groupName: group.groupName }}
      preload="intent"
    >
      <Card className="w-96 transition ease-in-out delay-50 hover:scale-105 hover:cursor-pointer min-h-40 ">
        <CardHeader>
          <CardTitle>{group.groupName}</CardTitle>
        </CardHeader>
        <CardContent>
          {group.players.length === 0 && (
            <p className="text-gray-400 text-sm">No players in this group!</p>
          )}
          {group.players.length > 0 &&
            group.players.slice(0, 2).map((player, index) => (
              <div key={player.playerName + index + group.groupName}>
                <p className="text-gray-400 text-sm">{player.playerName}</p>
                {group.players.length > 3 && index === 1 && (
                  <p className="text-gray-400 text-sm">...</p>
                )}
              </div>
            ))}
        </CardContent>
      </Card>
    </Link>
  );
};
export default GroupCard;
