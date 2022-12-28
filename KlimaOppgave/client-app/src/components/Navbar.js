import React from "react";
import { NavLink } from "react-router-dom";

const Navbar = () => {
  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary">
      <div className="container-sm">
        <NavLink className="navbar-brand" to="/">
          AskKlima
        </NavLink>
        <div className="collapse navbar-collapse">
          <ul
            className="navbar-nav justify-content-between w-25"
            style={{ marginLeft: "auto" }}
          >
            <li className="nav-item">
              <NavLink className="btn btn-primary" to="askquestion">
                Spør
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="btn btn-danger" to="signout">
                Logg ut
              </NavLink>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
