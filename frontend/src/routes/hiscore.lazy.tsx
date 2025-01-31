import { useState } from "react";
import { createLazyFileRoute } from "@tanstack/react-router";

import Layout from "@/components/Layout";
import MainTable from "@/components/MainTable";
import Search from "@/components/Search";
import Alert from "@/components/Alert";
import usePlayerHiscore from "@/hooks/usePlayerHiscore";

export const Route = createLazyFileRoute("/hiscore")({
  component: RouteComponent,
});

function RouteComponent() {
  const [name, setName] = useState("");
  const { data, isLoading, isError } = usePlayerHiscore(name);

  return (
    <Layout title="Hiscore">
      <div className="mt-4 flex flex-col gap-8 w-1/2">
        <Search
          id={"username-input"}
          label={"Username"}
          onSearch={(value) => setName(value)}
        />
        {isError && (
          <Alert
            title="Error"
            description={`An error occured when fetching the hiscore for ${name}`}
          />
        )}
        {
          <MainTable
            headings={["Skill", "Level", "XP", "Rank"]}
            tableData={data?.skills}
            isLoading={isLoading}
          />
        }
      </div>
    </Layout>
  );
}
