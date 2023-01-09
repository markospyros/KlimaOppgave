import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import AccountForm from "../components/AccountForm";

const Login = () => {
  const [brukernavn, setBrukernavn] = useState("");
  const [passord, setPassord] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [brukernavnErrorMessage, setBrukernavnErrorMessage] = useState("");
  const [passordErrorMessage, setPassordErrorMessage] = useState("");
  const [buttonStatus, setButtonStatus] = useState("Logg inn");

  const navigate = useNavigate();

  const onChangeBrukernavn = (event) => {
    const inputValue = event.target.value;
    setBrukernavn(inputValue);

    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;

    if (!regexp.test(inputValue)) {
      setBrukernavnErrorMessage("Brukernavnet må bestå av 2 til 20 bokstaver");
    } else {
      setBrukernavnErrorMessage("");
    }
  };

  const onChangePassord = (event) => {
    const inputValue = event.target.value;
    setPassord(event.target.value);

    const regexp = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/;

    if (!regexp.test(inputValue)) {
      setPassordErrorMessage(
        "Passordet må bestå minimum 6 tegn, minst en bokstav og et tall"
      );
    } else {
      setPassordErrorMessage("");
    }
  };

  const login = async (event) => {
    const user = { brukernavn: brukernavn, passord: passord };

    if (brukernavn !== "" && passord !== "") {
      axios
        .post("/logginn", user)
        .then((res) => {
          if (res.data === true) {
            setButtonStatus(
              <div
                className="spinner-border spinner-border-sm text-light"
                role="status"
              >
                <span className="visually-hidden"></span>
              </div>
            );
            setTimeout(() => {
              navigate("/");
            }, 1000);
          } else {
            setErrorMessage(
              "Kunne ikke logge inn med det brukenavnet. Vennligst prøv igjen."
            );
          }
        })
        .catch((error) => {
          setErrorMessage(error.response.data);
        });
    }

    if (brukernavn === "") {
      setBrukernavnErrorMessage("Brukernavn må fylles ut");
    }
    if (passord === "") {
      setPassordErrorMessage("Passord må fylles ut");
    }

    event.preventDefault();
  };

  return (
    <>
      <AccountForm
        formTitle="Logg inn"
        onSubmit={login}
        brukernavn={brukernavn}
        passord={passord}
        brukernavnErrorMessage={brukernavnErrorMessage}
        passordErrorMessage={passordErrorMessage}
        onChangeBrukernavn={onChangeBrukernavn}
        onChangePassord={onChangePassord}
        path="register"
        text="Har du ikke konto? Klikk her"
        errorMessage={errorMessage}
        buttonStatus={buttonStatus}
      />
    </>
  );
};

export default Login;
