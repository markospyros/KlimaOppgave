import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import AccountForm from "../components/AccountForm";

const Register = ({ setUser }) => {
  const [brukernavn, setBrukernavn] = useState("");
  const [passord, setPassord] = useState("");

  const navigate = useNavigate();

  const onChangeBrukernavn = (event) => {
    setBrukernavn(event.target.value);
  };

  const onChangePassord = (event) => {
    setPassord(event.target.value);
  };

  const createAccount = async (event) => {
    const user = { brukernavn: brukernavn, passord: passord };

    axios
      .post("/lagbruker", user)
      .then((res) => {
        user.brukerId = res.data;
        setUser(user);
        navigate("/");
      })
      .catch((error) => {
        console.log(error.response.data);
      });

    event.preventDefault();
  };

  return (
    <>
      <AccountForm
        formTitle="Registrer"
        onSubmit={createAccount}
        brukernavn={brukernavn}
        passord={passord}
        onChangeBrukernavn={onChangeBrukernavn}
        onChangePassord={onChangePassord}
        path="login"
        text="Har du allerede konto? Klikk her"
        buttonText="Registrer"
      />
    </>
  );
};

export default Register;
