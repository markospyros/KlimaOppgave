import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import Form from "../components/Form";

const AskQuestion = () => {
  const [tittel, setTittel] = useState("");
  const [innhold, setInnhold] = useState("");

  const onChangeTittel = (event) => {
    setTittel(event.target.value);
  };

  const onChangeInnhold = (event) => {
    setInnhold(event.target.value);
  };

  const navigate = useNavigate();

  const onSubmit = (event) => {
    const sporsmal = { tittel: tittel, innhold: innhold };

    axios.post("/legginnlegg", sporsmal);

    event.preventDefault();

    navigate("/");
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
        buttonTitle="Send inn"
      />
    </div>
  );
};

export default AskQuestion;
