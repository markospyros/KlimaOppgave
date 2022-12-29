import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import Form from "../components/Form";

const AskQuestion = () => {
  const [tittel, setTittel] = useState("");
  const [innhold, setInnhold] = useState("");
  const [buttonStatus, setButtonStatus] = useState("Send inn");

  const onChangeTittel = (event) => {
    setTittel(event.target.value);
  };

  const onChangeInnhold = (event) => {
    setInnhold(event.target.value);
  };

  const navigate = useNavigate();

  const onSubmit = () => {
    const sporsmal = { tittel: tittel, innhold: innhold };

    axios.post("/legginnlegg", sporsmal);

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
  };

  return (
    <div>
      <Form
        pageTitle="Still et spørsmål"
        onChangeTittel={onChangeTittel}
        onChangeInnhold={onChangeInnhold}
        valueTittel={tittel}
        valueInnhold={innhold}
        onSubmit={onSubmit}
        buttonTitle={buttonStatus}
      />
    </div>
  );
};

export default AskQuestion;
