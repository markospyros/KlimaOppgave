import axios from "axios";
import React, { useState } from "react";
import { BiEdit, BiTrash } from "react-icons/bi";
import { useNavigate } from "react-router-dom";
import ChangeAnswerModal from "./ChangeAnswerModal";
import DeleteModal from "./DeleteModal";
import ProfileCard from "./ProfileCard";

const AnswerComponent = (props) => {
  const [visibility, setVisibility] = useState("none");
  const [showModalChange, setShowModalChange] = useState(false);
  const [showModalDelete, setShowModalDelete] = useState(false);

  const onHoverAnswer = () => {
    if (props.sessionBrukernavn === props.brukernavn) {
      setVisibility("block");
    } else if (props.sessionBrukernavn === "Admin") {
      setVisibility("block");
    }
  };

  const onLeaveAnswer = () => {
    setVisibility("none");
  };

  const openModalChange = () => {
    setShowModalChange(true);
  };

  const closeModalChange = () => {
    setShowModalChange(false);
  };

  const openModalDelete = () => {
    setShowModalDelete(true);
  };

  const closeModalDelete = () => {
    setShowModalDelete(false);
  };

  const navigate = useNavigate();

  const onDelete = (id) => {
    axios
      .delete(`/slettsvar/${id}`)
      .then(() => props.deleteAnswer(id))
      .catch((error) => {
        if (error.response.status === 401) {
          navigate("/login");
        }
      });
  };

  return (
    <div
      className="card-footer"
      onMouseEnter={onHoverAnswer}
      onMouseLeave={onLeaveAnswer}
    >
      <div className="row">
        <div className="col-9">
          <ProfileCard
            brukernavn={props.brukernavn}
            picWidth={"45px"}
            picHeight={"45px"}
            fontSize={"fs-6 text"}
            marginBottom={"mb-3"}
          />
          <div>{props.svarInnhold}</div>
        </div>
        <div className={`col-3 text-end d-${visibility}`}>
          <BiEdit
            color="blue"
            size={20}
            className="mx-3"
            style={{ cursor: "pointer" }}
            onClick={openModalChange}
          />
          <BiTrash
            color="red"
            size={20}
            style={{ cursor: "pointer" }}
            onClick={openModalDelete}
          />
        </div>
        <div className="text-end text-secondary">{props.svarDato}</div>
      </div>
      <ChangeAnswerModal
        sessionBrukernavn={props.sessionBrukernavn}
        svarId={props.svarId}
        svarInnhold={props.svarInnhold}
        svarDato={props.svarDato}
        innleggId={props.innleggId}
        showModalChange={showModalChange}
        updateAnswer={props.updateAnswer}
        closeModalChange={closeModalChange}
      />
      <DeleteModal
        sentence="Er du sikker på at du vil slette svaret ditt?"
        showModalDelete={showModalDelete}
        closeModalDelete={closeModalDelete}
        onDelete={() => onDelete(props.svarId)}
      />
    </div>
  );
};

export default AnswerComponent;
