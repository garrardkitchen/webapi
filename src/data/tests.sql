USE WebApiDB;

# Create and update user
CALL sp_ins_user('Garrard', 'Kitchen', 'garrardkitchen@gamil.com', 'ABC');
CALL sp_sel_user('garrardkitchen@gamil.com');
CALL sp_upd_user('GarrardP', 'Kitchen', 'garrardkitchen@gamil.com', 'ABC');
CALL sp_sel_user('garrardkitchen@gamil.com');
CALL sp_upd_user('Garrard', 'Kitchen', 'garrardkitchen@gamil.com', 'ABC');
CALL sp_sel_user('garrardkitchen@gamil.com');

# Expect a duplicate error as email already exists
CALL sp_ins_user('Garrard', 'Kitchen', 'garrardkitchen@gamil.com', 'ABC');