import React from "react";
import { Outlet } from "react-router-dom";
import Navbar from "../components/Navbar";

const SharedLayout = (props) => {
  return (
    <>
      <Navbar brukernavn={props.sessionBrukernavn} />
      <div className="container">
        <div className="row">
          <div className="col-md-5 m-auto">
            <div className="mt-3">
              <Outlet />
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default SharedLayout;
