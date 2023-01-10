import axios from "axios";
import React from "react";
import { NavLink, useNavigate } from "react-router-dom";
import ProfileCard from "./ProfileCard";

const Navbar = (props) => {
  const navigate = useNavigate();

  const loggut = () => {
    axios.get("/loggut").then(navigate("/login"));
  };
  console.log(props.brukernavn);

  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary">
      <div className="container-sm">
        <NavLink className="navbar-brand" to="/">
          KlimaOverflow
        </NavLink>
        <div className="collapse navbar-collapse">
          <ul
            className="navbar-nav justify-content-between w-25"
            style={{ marginLeft: "auto" }}
          >
            <li className="nav-item">
              <ProfileCard
                brukernavn={props.brukernavn}
                picWidth="40px"
                picHeight="40px"
              />
            </li>
            <li className="nav-item">
              <button className="btn btn-danger" onClick={loggut}>
                Logg ut
              </button>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
