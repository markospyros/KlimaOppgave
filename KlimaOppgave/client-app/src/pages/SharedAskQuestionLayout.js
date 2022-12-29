import React from "react";
import { Outlet } from "react-router-dom";

const SharedAskQuestionLayout = () => {
  return (
    <>
      <Outlet />
    </>
  );
};

export default SharedAskQuestionLayout;
