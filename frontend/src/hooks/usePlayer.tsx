import { fetchPlayer } from "@/api/player";
import { useQuery } from "@tanstack/react-query";

const usePlayer = (name: string) => {
  return useQuery({
    queryKey: ["get-player", name],
    queryFn: () => fetchPlayer(name),
    enabled: !!name,
    retry: 0,
    staleTime: 1000 * 60 * 5, // 5 minutes
  });
};

export default usePlayer;
