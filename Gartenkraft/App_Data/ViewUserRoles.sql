CREATE VIEW [dbo].vwUserRoles
	AS select ur.UserId, u.UserName, r.Name from AspNetUserRoles ur
		join AspNetUsers u on u.Id=ur.UserId
		join AspNetRoles r on r.Id=ur.RoleId
